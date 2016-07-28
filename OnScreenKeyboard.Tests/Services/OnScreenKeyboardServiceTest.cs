using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnScreenKeyboard.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnScreenKeyboard.Tests.Services
{
    [TestClass]
    public class OnScreenKeyboardServiceTest
    {
        private const string keyboardLayout = "ABCDEF\nGHIJKL\nMNOPQR\nSTUVWX\nYZ1234\n567890";

        [TestMethod]
        public void CalculateResults_WithValidInput_ReturnsList()
        {
            // Arrange
            OnScreenKeyboardService service = new OnScreenKeyboardService();
            
            string searchTerms = "ABCD GHIJ";
            List<char> expectedResult = new List<char> { '#','R','#','R','#','R','#','S','D','L','L','L','#','R','#','R','#','R','#' };

            // Act
            List<char> results = service.CalculateResults(keyboardLayout, searchTerms);

            // Assert
            CollectionAssert.AreEqual(expectedResult, results);
        }

        [TestMethod]
        public void CalculateResults_WithLowerCaseSearchTerms_ReturnsList()
        {
            // Arrange
            OnScreenKeyboardService service = new OnScreenKeyboardService();
            string searchTerms = "Abcd GhIJ";
            List<char> expectedResult = new List<char> { '#', 'R', '#', 'R', '#', 'R', '#', 'S', 'D', 'L', 'L', 'L', '#', 'R', '#', 'R', '#', 'R', '#' };

            // Act
            List<char> results = service.CalculateResults(keyboardLayout, searchTerms);

            // Assert
            CollectionAssert.AreEqual(expectedResult, results);
        }

        [TestMethod]
        public void CalculateResults_WithInvalidSearchTerms_IgnoresThemAndReturnsList()
        {
            // Arrange
            OnScreenKeyboardService service = new OnScreenKeyboardService();
            string searchTerms = "ABCD !~`-GHIJ";
            List<char> expectedResult = new List<char> { '#', 'R', '#', 'R', '#', 'R', '#', 'S', 'D', 'L', 'L', 'L', '#', 'R', '#', 'R', '#', 'R', '#' };

            // Act
            List<char> results = service.CalculateResults(keyboardLayout, searchTerms);

            // Assert
            CollectionAssert.AreEqual(expectedResult, results);
        }

        [TestMethod]
        public void CalculateResults_WithLowerCaseLayout_ReturnsListWithOnlySpaces()
        {
            // Arrange
            OnScreenKeyboardService service = new OnScreenKeyboardService();
            string lowerCaseKeyboardLayout = "abcdef\nghijkl\nmnopqr\nstuvwx\nyz1234\n567890";
            string searchTerms = "abcd ghij";
            List<char> expectedResult = new List<char> { 'S' };

            // Act
            List<char> results = service.CalculateResults(lowerCaseKeyboardLayout, searchTerms);

            // Assert
            CollectionAssert.AreEqual(expectedResult, results);
        }
    }
}
