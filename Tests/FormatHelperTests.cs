using Data2Web.Logic.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class FormatHelperTests
    {
        [Fact]
        public void FormatDate_ShouldReturnDateInCorrectFormat()
        {
            
            var date = new DateTime(2025, 9, 25);

            var result = FormatHelper.FormatDate(date);

            Assert.Equal("25/09/2025", result);
        }

        [Fact]
        public void Capitalize_ShouldCapitalizeText()
        {
           
            var input = "erick";

           
            var result = FormatHelper.Capitalize(input);

           
            Assert.Equal("Erick", result);
        }

        [Fact]
        public void Capitalize_ShouldReturnEmptyString_WhenInputIsNullOrEmpty()
        {
            
            string? input = null;

           
            var result = FormatHelper.Capitalize(input);

            
            Assert.Equal(string.Empty, result);
        }
    }

}
