using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;
using AttendanceAPI.Models;

namespace AttendanceAPI
{

    internal class DataLayer
    {

        #region "Properties"

        /// <summary>
        /// This variable holds the connection details
        /// such as name of database server, database name, username, and password.
        /// The ? makes the property nullable
        /// </summary>
        private readonly string? connectionString = null;

        #endregion

        #region "Constructors"

        /// <summary>
        /// This is the default constructor and has the default 
        /// connection string specified 
        /// </summary>
        public DataLayer()
        {
            //preprocessor directives can help by using a debug build database environment for testing
            // while using a production database environment for production build.
#if (DEBUG)
            //connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            connectionString = @"Data Source = (localdb)\ProjectModels; Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt = False;";
#else
            connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"; 
#endif
        }

        /// <summary>
        /// Parameterized Constructor passing in a connection string
        /// </summary>
        /// <param name="connectionString"></param>
        public DataLayer(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #endregion

        #region "Database Operations"

        /// <summary>
        /// Get all Student in the database and return in a List
        /// </summary>
        /// <param>None</param>
        /// <returns>List of Students</returns>
        /// <exception cref="Exception"></exception>
        public List<UserDTO> GetStudents()
        {
            List<UserDTO> Students = new();

            try
            {

                //using guarentees the release of resources at the end of scope 
                using SqlConnection conn = new(connectionString);

                // open the database connection
                conn.Open();

                // create a command object identifying the stored procedure
                using SqlCommand cmd = new SqlCommand("spGetStudents", conn);

                // set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // execute the command which returns a data reader object
                // usually we should use ExecuteReaderAsync() but for this example we will use ExecuteReader()
                using SqlDataReader rdr = (SqlDataReader)cmd.ExecuteReader();

                // if the reader contains a data set, convert to Student objects
                while (rdr.Read())
                {
                    UserDTO Student = new UserDTO();

                    Student.Guid = (string)rdr.GetValue(2);
                    Student.UserName = (string)rdr.GetValue(1);
                    

                    Students.Add(Student);
                }
            }
            catch (Exception)
            {
                throw;
            }

            //check for Students length to be zero after returned from database
            return Students;

        } // end function GetStudents

        /// <summary>
        /// Get a user level by key (GUID)
        /// returns the users access level
        /// </summary>
        /// <param name="key"></param>
        /// <returns>UserLevelDTO</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public UserLevelDTO? GetUserLevelByKey(string key)
        {

            UserLevelDTO? levelDTO = null;

            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException("Username or Password can not be null.");
                }

                //using guarentees the release of resources at the end of scope 
                using SqlConnection conn = new SqlConnection(connectionString);

                // open the database connection
                conn.Open();

                // create a command object identifying the stored procedure
                using SqlCommand cmd = new SqlCommand("spGetUserLevel", conn);

                // set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // add parameters to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("UserKey", key));

                // execute the command which returns a data reader object
                using SqlDataReader rdr = (SqlDataReader)cmd.ExecuteReader();

                // if the reader contains a data set, load a local user object
                if (rdr.Read())
                {
                    levelDTO = new();
                    levelDTO.UserLevel = (string)rdr.GetValue(0);
                    levelDTO.Guid = (string)rdr.GetValue(1);
                }
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return levelDTO;    

        } // end function GetUserLevelByKey

        /// <summary>
        /// Gets an Student in the database by student guid and returns an StudentDTO or null
        /// </summary>
        /// <param>Id</param>
        /// <returns>StudentDTO</returns>
        /// <exception cref="Exception"></exception>
        public UserDTO? GetStudentByGuid(string guid)
        {
            UserDTO? StudentDTO = null;

            try
            {

                //using guarentees the release of resources at the end of scope 
                using SqlConnection conn = new(connectionString);

                // open the database connection
                conn.Open();

                // create a command object identifying the stored procedure
                using SqlCommand cmd = new SqlCommand("spGetAnStudent", conn);

                // set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // add parameters to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@Guid", guid));

                // execute the command which returns a data reader object
                // usually we should use ExecuteReaderAsync() but for this example we will use ExecuteReader()
                using SqlDataReader rdr = (SqlDataReader)cmd.ExecuteReader();

                // if the reader contains a data set, convert to Student objects
                if (rdr.Read())
                {
                    //Student is null so create a new instance
                    StudentDTO = new UserDTO();

                    StudentDTO.Guid = (string)rdr.GetValue(0);
                    StudentDTO.UserName = (string)rdr.GetValue(1);                  
                }
            }
            catch (Exception)
            {
                //rethrow exception
                throw;
            }
            //check for Students length to be zero after returned from database
            return StudentDTO;

        } // end function GetStudentById

        /// <summary>
        /// Insert an Student into the database and return the Student with the new ID
        /// </summary>
        /// <param>Student</param>
        /// <returns>Student</returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="ArgumentNullException"></exception>"
        public UserDTO? InsertStudent(UserDTO Student)
        {

            UserDTO? tempStudent = null;
            try
            {
                if (Student == null)
                {
                    throw new ArgumentNullException("Student can not be null.");
                }

                //using guarentees the release of resources at the end of scope 
                using SqlConnection conn = new(connectionString);

                // open the database connection
                conn.Open();

                // create a command object identifying the stored procedure
                using SqlCommand cmd = new SqlCommand("spInsertStudent", conn);

                // set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // add parameters to command, which will be passed to the stored procedure          
                cmd.Parameters.Add(new SqlParameter("@Guid", Student.Guid));
                cmd.Parameters.Add(new SqlParameter("@UserName", Student.UserName));

                //create a parameter to hold the output value
                SqlParameter IdValue = new SqlParameter("@Id", SqlDbType.Int);
                IdValue.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(IdValue);

                // execute the command which returns a data reader object
                // usually we should use ExecuteReaderAsync() but for this example we will use ExecuteReader()
                int count = cmd.ExecuteNonQuery();

                // if the reader contains a data set, convert to Student objects
                if (count > 0)
                {
                    //Student is null so create a new instance
                    tempStudent = new UserDTO();

                    tempStudent.UserName = (string)IdValue.Value;
                }
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

            //Student is null if there was an error
            return tempStudent;

        } // end function GetStudentById

        /// <summary>
        /// Update an Student in the database and return row count affected
        /// </summary>
        /// <param>Id, StudentDTO</param>
        /// <returns>int</returns>
        /// <exception cref="Exception"></exception>
        public int UpdateStudent(int Id, UserDTO StudentDTO)
        {
            int count;

            try
            {
                if (StudentDTO == null)
                {
                    throw new ArgumentNullException("Student can not be null.");
                }

                //using guarentees the release of resources at the end of scope 
                using SqlConnection conn = new(connectionString);

                // open the database connection
                conn.Open();

                // create a command object identifying the stored procedure
                using SqlCommand cmd = new SqlCommand("spUpdateStudent", conn);

                // set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // add parameters to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@Guid", StudentDTO.Guid));
                cmd.Parameters.Add(new SqlParameter("@Title", StudentDTO.UserName));

                // execute the command which returns a data reader object
                // usually we should use ExecuteReaderAsync() but for this example we will use ExecuteReader()
                count = cmd.ExecuteNonQuery();

            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                // could write log here
                throw;
            }
            finally
            {
                // no clean up because the 'using' statements guarantees closing resources
            }

            //Student is null if there was an error
            return count;

        } // end function GetStudentById

        /// <summary>
        /// Delete an Student in the database and return row count affected
        /// </summary>
        /// <param>Id</param>
        /// <returns>int</returns>
        /// <exception cref="Exception"></exception>
        public int DeleteStudent(int Guid)
        {
            int count;
            try
            {
                //using guarentees the release of resources at the end of scope 
                using SqlConnection conn = new(connectionString);

                // open the database connection
                conn.Open();

                // create a command object identifying the stored procedure
                using SqlCommand cmd = new SqlCommand("dbo.spDeleteStudent", conn);

                // set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // add parameters to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@Guid", Guid));

                count = cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            return count;
        } // end function GetStudentById



        #endregion

    } // end class DataLayer

}
