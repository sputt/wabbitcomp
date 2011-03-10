﻿using WabbitC.TokenPasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WabbitC;
using System.Collections.Generic;
using System.Linq;

namespace WabbitC_Tests
{
    
    
    /// <summary>
    ///This is a test class for CompoundAssignmentRemoverTest and is intended
    ///to contain all CompoundAssignmentRemoverTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CompoundAssignmentRemoverTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Run
        ///</summary>
        [TestMethod()]
        public void RunTest1()
        {
            Tokenizer tokenizer = new Tokenizer();
            CompoundAssignmentRemover target = new CompoundAssignmentRemover(); // TODO: Initialize to an appropriate value

            tokenizer.Tokenize("test += 20;");
            List<Token> tokenList = tokenizer.Tokens; // TODO: Initialize to an appropriate value

            tokenizer.Tokenize("test = test + (20);");
            List<Token> expected = tokenizer.Tokens;

            List<Token> actual;
            actual = target.Run(tokenList);

            Assert.IsTrue(expected.SequenceEqual<Token>(actual),
                "Expected: \"" + string.Join<Token>("", expected.ToArray()) + "\" " +
                "Actual: \"" + string.Join<Token>("", actual.ToArray()) + "\"");

        }

        [TestMethod()]
        public void RunTest2()
        {
            Tokenizer tokenizer = new Tokenizer();
            CompoundAssignmentRemover target = new CompoundAssignmentRemover(); // TODO: Initialize to an appropriate value

            tokenizer.Tokenize("test -= 20;");
            List<Token> tokenList = tokenizer.Tokens; // TODO: Initialize to an appropriate value

            tokenizer.Tokenize("test = test - (20);");
            List<Token> expected = tokenizer.Tokens;

            List<Token> actual;
            actual = target.Run(tokenList);

            Assert.IsTrue(expected.SequenceEqual<Token>(actual), 
                "Expected: \"" + string.Join<Token>("", expected.ToArray()) + "\" " + 
                "Actual: \"" + string.Join<Token>("", actual.ToArray()) + "\"");
        }
    }
}