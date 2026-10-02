// Interfaces para Moq

// ITextService.cs
public interface ITextService
{
    bool IsValidEmail(string email);
    bool IsValidUrl(string url);
    bool IsValidPhone(string phone, string countryCode = "US");
    string Normalize(string input);
    bool ContainsOnlyAlphanumeric(string input);
    bool IsPalindrome(string input);
    int CountVowels(string input);
    string Reverse(string input);
}