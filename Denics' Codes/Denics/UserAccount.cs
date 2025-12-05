/*
 * A class to remember user account information.
 * Recives user ID upon login in LogInPage and stores it.
 * Sends user ID to other forms when needed.
 */

using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Denics
{
    /// <summary>
    /// Static holder for the current user's account information.
    /// Call <see cref="SetUserId(int)"/> after successful login to populate details.
    /// </summary>
    public static class UserAccount
    {
        // Nullable so "no user" can be represented
        private static int? _userId;
        private static string surname;
        private static string firstname;
        private static string middlename;
        private static string suffix;
        private static string birthdate;      // corrected spelling and will store formatted date
        private static string gender;
        private static string email;
        private static string contactnumber; // store contact as string (preserve leading zeros)
        private static string address;

        // Set the current logged-in user
        public static void SetUserId(int userId)
        {
            _userId = userId;
            // Load other user information from database
            SetUserInfoFromDatabase(userId);
        }

        // Convenience: get id or 0 when none
        public static int GetUserIdOrDefault() => _userId ?? 0;

        // Clear the current user (logout)
        public static void Clear()
        {
            _userId = null;
            // clear other details as well
            surname = firstname = middlename = suffix = birthdate = gender = email = address = string.Empty;
            contactnumber = string.Empty;
        }

        // Other Information
        // contact now as string; birth (string) will contain only day/month/year formatted value
        public static void SetUserDetails(string sur, string first, string middle, string suff, string birth, string gen, string mail, string contact, string addr)
        {
            surname = sur;
            firstname = first;
            middlename = middle;
            suffix = suff;
            birthdate = birth;
            gender = gen;
            email = mail;
            contactnumber = contact;
            address = addr;
        }

        public static string GetSurname() => surname;
        public static string GetFirstname() => firstname;
        public static string GetMiddlename() => middlename;
        public static string GetSuffix() => suffix;
        // returns formatted birth date (day/month/year) or empty string
        public static string GetBirthdate() => birthdate;
        public static string GetGender() => gender;
        public static string GetEmail() => email;
        // contact returned as string to preserve formatting and length (e.g. "09999999999")
        public static string GetContactnumber() => contactnumber;
        public static string GetAddress() => address;

        // Database helper (local to methods to avoid holding connections)
        private static void SetUserInfoFromDatabase(int userId)
        {
            // Use a local connection/command to keep the class thread-friendlier and avoid long-lived connection fields.
            try
            {
                var db = new CallDatabase();
                using (var con = new SqlConnection(db.getDatabaseStringName()))
                {
                    con.Open();
                    string query = @"SELECT surname, firstname, middlename, suffix, birthdate, gender, email, contact, address 
                                     FROM Users 
                                     WHERE user_id = @UserId";

                    using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string rFirstname = reader["firstname"]?.ToString() ?? string.Empty;
                                string rSurname = reader["surname"]?.ToString() ?? string.Empty;
                                string rMiddlename = reader["middlename"]?.ToString() ?? string.Empty;
                                string rSuffix = reader["suffix"]?.ToString() ?? string.Empty;

                                // birthdate is stored as DATE in DB — format to day/month/year only
                                string rBirthdate = string.Empty;
                                if (reader["birthdate"] != DBNull.Value)
                                {
                                    if (DateTime.TryParse(reader["birthdate"].ToString(), out DateTime bd))
                                    {
                                        // choose format you want; here: "MMMM dd, yyyy" (e.g. January 01, 2000)
                                        rBirthdate = bd.ToString("MMMM dd, yyyy");
                                    }
                                }

                                string rGender = reader["gender"]?.ToString() ?? string.Empty;
                                string rEmail = reader["email"]?.ToString() ?? string.Empty;

                                // contact is nvarchar in DB — keep as string to preserve leading zeros and length
                                string rContact = reader["contact"]?.ToString() ?? string.Empty;

                                string rAddress = reader["address"]?.ToString() ?? string.Empty;

                                // Set user details in UserAccount
                                SetUserDetails(rSurname, rFirstname, rMiddlename, rSuffix, rBirthdate, rGender, rEmail, rContact, rAddress);
                            }
                            else
                            {
                                MessageBox.Show("User details not found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user details: " + ex.Message);
            }
        }
    }
}
