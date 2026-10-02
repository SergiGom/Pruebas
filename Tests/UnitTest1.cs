using DDL.Clase_Basica;
using DDL.Escenarios_Avanzados;
using DDL.Interfaces_para_Moq;
using FluentAssertions;
using Moq;
using System.Text.RegularExpressions;
using System.Timers;

namespace Tests
{

    public class TextServiceTests
    {
        private readonly Mock<IRegexService> _mockRegex;
        private readonly TextService _sut;

        public TextServiceTests()
        {
            _mockRegex = new Mock<IRegexService>();
            _sut = new TextService(_mockRegex.Object);
        }

        [Theory]
        [InlineData("user@example.com", true)]
        [InlineData("invalid-email", false)]
        public void IsValidEmail_ValidInput_ReturnsExpected(string email, bool expected)
        {
            // Arrange
            _mockRegex.Setup(r => r.IsMatch(email, It.IsAny<string>(), It.IsAny<RegexOptions>()))
                      .Returns(expected);

            // Act
            var result = _sut.IsValidEmail(email);

            // Assert
            result.Should().Be(expected);
            _mockRegex.Verify(r => r.IsMatch(email, It.IsAny<string>(), It.IsAny<RegexOptions>()), Times.Once);
        }

        [Fact]
        public void IsValidEmail_EmptyInput_ReturnsFalse_WithoutCallingRegex()
        {
            // Act
            var result = _sut.IsValidEmail("");

            // Assert
            result.Should().BeFalse();
            _mockRegex.Verify(r => r.IsMatch(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RegexOptions>()), Times.Never);
        }

        [Fact]
        public void IsPalindrome_ReturnsTrueForPalindrome()
        {
            var result = _sut.IsPalindrome("A man a plan a canal Panama");
            result.Should().BeTrue();
        }

        [Fact]
        public void CountVowels_ReturnsCorrectCount()
        {
            var result = _sut.CountVowels("Hello World");
            result.Should().Be(3);
        }
    }

    public class WildcardServiceTests
    {
        private readonly WildcardService _sut;

        public WildcardServiceTests()
        {
            _sut = new WildcardService();
        }

        [Theory]
        [InlineData("file.txt", "*.txt", true)]
        [InlineData("file.csv", "*.txt", false)]
        [InlineData("file1.txt", "file?.txt", true)]
        [InlineData("file12.txt", "file?.txt", false)]
        public void Matches_ReturnsExpected(string input, string pattern, bool expected)
        {
            var result = _sut.Matches(input, pattern);
            result.Should().Be(expected);
        }

        [Fact]
        public void Filter_ReturnsOnlyMatchingItems()
        {
            var files = new[] { "a.txt", "b.csv", "c.txt", "d.log" };
            var result = _sut.Filter(files, "*.txt").ToList();
            result.Should().HaveCount(2).And.ContainInOrder("a.txt", "c.txt");
        }
    }

    public class AdvancedScenariosTests
    {
        private readonly Mock<IRegexService> _mockRegex;
        private readonly Mock<ITextService> _mockText;
        private readonly Mock<IDateService> _mockDate;
        private readonly Mock<ITimeService> _mockTime;
        private readonly Mock<ICollectionService> _mockCollection;
        private readonly Mock<IWildcardService> _mockWildcard;
        private readonly AdvancedScenarios _sut;

        public AdvancedScenariosTests()
        {
            _mockRegex = new Mock<IRegexService>();
            _mockText = new Mock<ITextService>();
            _mockDate = new Mock<IDateService>();
            _mockTime = new Mock<ITimeService>();
            _mockCollection = new Mock<ICollectionService>();
            _mockWildcard = new Mock<IWildcardService>();
            _sut = new AdvancedScenarios(
                _mockText.Object, _mockDate.Object, _mockTime.Object,
                _mockCollection.Object, _mockRegex.Object, _mockWildcard.Object);
        }

        [Fact]
        public void IsValidIPv4_ValidIp_ReturnsTrue()
        {
            // Arrange
            _mockRegex.Setup(r => r.IsMatch("192.168.1.1", It.IsAny<string>(), It.IsAny<RegexOptions>()))
                      .Returns(true);

            // Act
            var result = _sut.IsValidIPv4("192.168.1.1");

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsValidIPv4_InvalidIp_ReturnsFalse()
        {
            var result = _sut.IsValidIPv4("999.999.999.999");
            result.Should().BeFalse();
        }

        [Fact]
        public void MaskCreditCard_MasksCorrectly()
        {
            var result = _sut.MaskCreditCard("4111111111111111");
            result.Should().Be("****-****-****-1111");
        }

        [Fact]
        public void IsValidPassword_WeakPassword_ReturnsFalse()
        {
            var result = _sut.IsValidPassword("abc");
            result.Should().BeFalse();
        }

        [Fact]
        public void IsValidPassword_StrongPassword_ReturnsTrue()
        {
            _mockRegex
                .Setup(r => r.IsMatch(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RegexOptions>()))
                .Returns(true);

            var result = _sut.IsValidPassword("Str0ng!Pass");
            result.Should().BeTrue();
        }

        [Fact]
        public void HasPotentialSqlInjection_DetectsInjection()
        {
            _mockRegex
                .Setup(r => r.IsMatch(It.IsAny<string>(), It.IsAny<string>(), RegexOptions.IgnoreCase))
                .Returns(true);

            var result = _sut.HasPotentialSqlInjection("1; DROP TABLE users--");
            result.Should().BeTrue();
        }

        [Fact]
        public void IsValidSemver_ValidVersion_ReturnsTrue()
        {
            _mockRegex
                .Setup(r => r.IsMatch("1.2.3", It.IsAny<string>(), It.IsAny<RegexOptions>()))
                .Returns(true);

            var result = _sut.IsValidSemver("1.2.3");
            result.Should().BeTrue();
        }

        [Fact]
        public void IsValidGuid_ValidGuid_ReturnsTrue()
        {
            var result = _sut.IsValidGuid("123e4567-e89b-12d3-a456-426614174000");
            result.Should().BeTrue();
        }

        [Fact]
        public void IsValidGuid_InvalidGuid_ReturnsFalse()
        {
            var result = _sut.IsValidGuid("not-a-guid");
            result.Should().BeFalse();
        }
    }


}
