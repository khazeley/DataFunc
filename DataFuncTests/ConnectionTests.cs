using System;
using System.Data;
using System.Data.SqlClient;
using DataFunc.Connections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataFuncTests
{
    using DataFunc;

    [TestClass]
    public class ConnectionTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToSqlConnection_Null_ArgumentNullException_()
        {
            string connectionString = null;
            connectionString.ToSqlConnection();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToSqlConnection_Empty_ArgumentNullException()
        {
            string.Empty.ToSqlConnection();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToSqlConnection_Whitespace_ArgumentNullException()
        {
            " ".ToSqlConnection();
        }

        [TestMethod]
        public void ToSqlConnection_ValidConnectionString_ReturnsConnection()
        {
            var connectionString = "Data Source=localhost;Initial Catalog=DonateNYC;Integrated Security=True";
            var result = connectionString.ToSqlConnection();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SqlConnection));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToOpenSqlConnection_Null_ArgumentNullException_()
        {
            string connectionString = null;
            connectionString.ToOpenSqlConnection();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToOpenSqlConnection_Empty_ArgumentNullException()
        {
            string.Empty.ToOpenSqlConnection();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToOpenSqlConnection_Whitespace_ArgumentNullException()
        {
            " ".ToOpenSqlConnection();
        }

        [TestMethod]
        public void ToOpenSqlConnection_ValidConnectionString_ReturnsOpenSqlConnection()
        {
            var connectionString = "Data Source=localhost;Initial Catalog=DonateNYC;Integrated Security=True";
            var result = connectionString.ToOpenSqlConnection();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SqlConnection));
            Assert.IsTrue(result.State == ConnectionState.Open);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToOpenSqlConnection_MalformedConnectionString_ArgumentException()
        {
            "D".ToOpenSqlConnection();
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void ToOpenSqlConnection_ClosedConnectionString_SqlException()
        {
            "Data Source=localhost;Initial Catalog=DonateNY;Integrated Security=True".ToOpenSqlConnection();
        }
    }
}
