using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Drawing.Imaging;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Rectangle = iTextSharp.text.Rectangle;

namespace Denics.Administrator
{
    // Small helper: generate a branded appointment PDF (style matches AppointmentService) and send via SMTP.
    // Intentionally keeps the simple inline SMTP pattern used in OTPVerification.
    internal static class AppointmentServices
    {
        public static void SendApprovedPdf(
            string recipientEmail,
            string patientName,
            string serviceType,
            DateTime apntDate,
            string apntTime,
            string doctor,
            string apntNote)
        {
            SendEmailOfType("Appointment Approved", "Confirmed", recipientEmail, patientName, serviceType, apntDate, apntTime, doctor, apntNote);
        }

        public static void SendDeniedEmail(string recipientEmail, string patientName, string serviceType, DateTime apntDate, string apntTime, string doctor, string apntNote)
            => SendEmailOfType("Appointment Denied", "Cancelled", recipientEmail, patientName, serviceType, apntDate, apntTime, doctor, apntNote);

        public static void SendCompletedEmail(string recipientEmail, string patientName, string serviceType, DateTime apntDate, string apntTime, string doctor, string apntNote)
            => SendEmailOfType("Appointment Completed", "Completed", recipientEmail, patientName, serviceType, apntDate, apntTime, doctor, apntNote);

        public static void SendNoShowEmail(string recipientEmail, string patientName, string serviceType, DateTime apntDate, string apntTime, string doctor, string apntNote)
            => SendEmailOfType("Appointment: No-Show", "No-Show", recipientEmail, patientName, serviceType, apntDate, apntTime, doctor, apntNote);

        public static void SendCancelledEmail(string recipientEmail, string patientName, string serviceType, DateTime apntDate, string apntTime, string doctor, string apntNote)
            => SendEmailOfType("Appointment Cancelled", "Cancelled", recipientEmail, patientName, serviceType, apntDate, apntTime, doctor, apntNote);

        public static void SendPendingEmail(string recipientEmail, string patientName, string serviceType, DateTime apntDate, string apntTime, string doctor, string apntNote)
            => SendEmailOfType("Appointment Pending", "Under Evaluation", recipientEmail, patientName, serviceType, apntDate, apntTime, doctor, "Thanks for reaching out! We're currently reviewing your appointment request. We will follow up with an update as soon as possible. You will receive an email notification once your appointment has either approved or cancelled.");

        // New: send reschedule email to user AND Denics (Denics email obtained via CallEmail)
        public static void SendRescheduleEmail(string recipientEmail, string patientName, string serviceType, DateTime apntDate, string apntTime, string doctor, string apntNote)
            => SendEmailOfType("Appointment Rescheduled", "Rescheduled", recipientEmail, patientName, serviceType, apntDate, apntTime, doctor, apntNote, includeDenics: true);

        // includeDenics: when true, adds Denics email (from CallEmail.getEmail()) to recipients
        private static void SendEmailOfType(string titleText, string bookingStatus, string recipientEmail, string patientName, string serviceType, DateTime apntDate, string apntTime, string doctor, string apntNote, bool includeDenics = false)
        {
            if (string.IsNullOrWhiteSpace(recipientEmail)) throw new ArgumentException("recipientEmail required", nameof(recipientEmail));

            string tempPdf = Path.Combine(Path.GetTempPath(), $"Denics_Appointment_{Guid.NewGuid():N}.pdf");

            try
            {
                // Generate branded PDF similar to AppointmentService.GeneratePdf
                // Fonts: try system TTF (Arial) for consistent rendering
                string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                BaseFont bf;
                if (File.Exists(fontPath))
                {
                    bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                }
                else
                {
                    bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                }

                var titleFont = new iTextSharp.text.Font(bf, 18, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                var headerInfoFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.NORMAL, BaseColor.WHITE);
                var labelFont = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);
                var valueFont = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                var smallFont = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);

                using (var fs = new FileStream(tempPdf, FileMode.Create, FileAccess.Write, FileShare.None))
                using (var doc = new Document(PageSize.A4, 36, 36, 36, 36))
                using (var writer = PdfWriter.GetInstance(doc, fs))
                {
                    doc.Open();

                    // Header: colored bar with centered logo
                    PdfPTable headerTbl = new PdfPTable(1) { WidthPercentage = 100f };
                    PdfPCell headerCell = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    headerCell.MinimumHeight = 80f;
                    headerCell.BackgroundColor = new BaseColor(100, 149, 237); // CornflowerBlue

                    try
                    {
                        using var ms = new MemoryStream();
                        var bmp = global::Denics.Properties.Resources.Logo__no_bg___Denics;
                        bmp?.Save(ms, ImageFormat.Png);
                        if (ms.Length > 0)
                        {
                            var logoImage = iTextSharp.text.Image.GetInstance(ms.ToArray());
                            logoImage.Alignment = Element.ALIGN_CENTER;
                            logoImage.ScaleToFit(120f, 60f);
                            headerCell.AddElement(logoImage);
                        }
                        else
                        {
                            headerCell.AddElement(new Phrase("Denics Dental Clinic", headerInfoFont));
                        }
                    }
                    catch
                    {
                        headerCell.AddElement(new Phrase("Denics Dental Clinic", headerInfoFont));
                    }

                    headerTbl.AddCell(headerCell);
                    doc.Add(headerTbl);

                    // Divider
                    var line = new LineSeparator(1f, 100f, BaseColor.GRAY, Element.ALIGN_CENTER, -2f);
                    doc.Add(new Chunk(line));
                    doc.Add(new Paragraph("\n"));

                    // Title
                    Paragraph title = new Paragraph(titleText, titleFont) { Alignment = Element.ALIGN_CENTER };
                    doc.Add(title);
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));

                    // Greeting and status line
                    doc.Add(new Paragraph($"Hello {patientName},", valueFont));
                    doc.Add(new Paragraph($"Your booking with Denics is \"{bookingStatus}\".", valueFont));
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));


                    // Details table
                    PdfPTable details = new PdfPTable(2) { WidthPercentage = 100f, SpacingBefore = 4f, SpacingAfter = 8f };
                    details.SetWidths(new float[] { 1f, 2f });

                    details.AddCell(CreateDetailCell("Service:", labelFont));
                    details.AddCell(CreateDetailCell(serviceType, valueFont));
                    details.AddCell(CreateDetailCell("Date:", labelFont));
                    details.AddCell(CreateDetailCell(apntDate.ToString("MMMM dd, yyyy"), valueFont));
                    details.AddCell(CreateDetailCell("Time:", labelFont));
                    details.AddCell(CreateDetailCell(apntTime, valueFont));
                    details.AddCell(CreateDetailCell("Doctor:", labelFont));
                    details.AddCell(CreateDetailCell(doctor, valueFont));
                    details.AddCell(CreateDetailCell("Appointment Type:", labelFont));
                    details.AddCell(CreateDetailCell(bookingStatus, valueFont));

                    doc.Add(details);

                    // Notes
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));
                    string notesToShow = string.IsNullOrWhiteSpace(apntNote) ? "N/A" : apntNote;
                    doc.Add(new Paragraph("Notes:", labelFont));
                    var notesPara = new Paragraph(notesToShow, valueFont) { SpacingAfter = 8f };
                    doc.Add(notesPara);

                    // Static remark
                    doc.Add(new Paragraph("We look forward to seeing you soon,\nDenics Dental Clinic", valueFont));
                    doc.Add(new Paragraph("\n"));

                    // Footer separator and policy text (smaller)
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));


                    var footerLine = new LineSeparator(0.5f, 100f, BaseColor.LIGHT_GRAY, Element.ALIGN_CENTER, -2f);
                    doc.Add(new Chunk(footerLine));
                    doc.Add(new Paragraph("\n"));


                    string policy = "If you need to cancel your appointment, we respectfully request at least 48 hours' notice. Any cancellation or rescheduling made less than 48 hours before the scheduled appointment will result in a cancellation fee. The amount of the fee will be equal to 15% of the reserved services or $20, whichever is more. We do understand that things do not always go as planned. In case of extraordinary circumstances, please call us directly on (02) 3456-7890 | +63 917 123 4567 to make an exception.";
                    var policyPara = new Paragraph(policy, smallFont) { Alignment = Element.ALIGN_JUSTIFIED };
                    doc.Add(policyPara);

                    doc.Close();
                    writer.Close();
                }

                // Send email (simple inline pattern consistent with OTPVerification)
                using (var message = new MailMessage())
                {
                    // get email from CallEmail
                    CallEmail callEmail = new CallEmail();
                    string email = callEmail.getEmail();
                    message.From = new MailAddress(email);

                    // Primary recipient
                    message.To.Add(recipientEmail);

                    // Optionally include Denics email as recipient (for reschedules)
                    if (includeDenics)
                    {
                        try
                        {
                            string denics = callEmail.getEmail();
                            if (!string.IsNullOrWhiteSpace(denics) && !string.Equals(denics, recipientEmail, StringComparison.OrdinalIgnoreCase))
                            {
                                message.To.Add(denics);
                            }
                        }
                        catch
                        {
                            // ignore and continue if CallEmail fails
                        }
                    }

                    message.Subject = $"{titleText} - {apntDate:MMMM dd, yyyy}";
                    message.Body = $"Hello {patientName},\n\nYour appointment status is \"{bookingStatus}\". Please find the attached PDF for details.\n\nRegards,\nDenics Dental Clinic";
                    message.IsBodyHtml = false;

                    message.Attachments.Add(new Attachment(tempPdf, "application/pdf"));

                    using (var client = new SmtpClient("smtp.gmail.com", 587))
                    {
                        client.EnableSsl = true;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        // inline credentials follow the OTPVerification pattern — replace with secure storage in production
                        client.Credentials = new NetworkCredential("denicsdental@gmail.com", "rhrd vytr lxih utoo\r\n");
                        client.Send(message);
                    }
                }
            }
            finally
            {
                // best-effort cleanup
                try { if (File.Exists(tempPdf)) File.Delete(tempPdf); } catch { /* ignore */ }
            }
        }

        private static PdfPCell CreateDetailCell(string text, iTextSharp.text.Font font)
        {
            var c = new PdfPCell(new Phrase(text, font))
            {
                Border = iTextSharp.text.Rectangle.NO_BORDER,
                Padding = 6f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };
            return c;
        }
    }
}