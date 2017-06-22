using System;
using NUnit.Framework;
using Moq;
using SimpleSequence;

namespace SimpleSequence.Tests
{
    [TestFixture]
    public class GeneratorTest
    {
        Mock<IIDStorage> _mockTextStorage;
        SequenceConfiguration _config;

        [OneTimeSetUp]
        public void Setup()
        {
            _config = new SequenceConfiguration()
            {
                IDFormat = "N",
                SequenceLenght = 5,
                FillerCharacter = '0',
                ApplicationName = "Test",
                Date = new DateTime(2017,2,2)
            };
        }
        [TestCase("N",1,"00001")]
        [TestCase("0N1",3,"AAA00003BBB")]
        [TestCase("Y-N{TEST}",5,"2017-00005TEST")]
        [TestCase("MDY-N",11,"02022017-00011")]
        [TestCase("N{-TEST}",210001,"10001-TEST")]
        public void GenerateSequenceTest(string format,long expectedSequence, string expected)
        {
            string[] customs = { "AAA", "BBB", "CCC" };

            _mockTextStorage = new Mock<IIDStorage>(MockBehavior.Strict);
            _mockTextStorage.Setup(p => p.GetNextSequenceID()).Returns(expectedSequence);

            _config.IDFormat = format;
            Generator systemUnderTest = new Generator(_mockTextStorage.Object, _config);
            var result = systemUnderTest.GenerateID(customs);

            Assert.That(result, Is.EqualTo(expected));

            _mockTextStorage.VerifyAll();
        }
    }

}
