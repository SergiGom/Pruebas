using DDL.Interfaces_para_Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DDL.Escenarios_Avanzados
{
    public class AdvancedScenarios
    {
        private readonly ITextService _text;
        private readonly IDateService _date;
        private readonly ITimeService _time;
        private readonly ICollectionService _collection;
        private readonly IRegexService _regex;
        private readonly IWildcardService _wildcard;

        public AdvancedScenarios(
            ITextService text,
            IDateService date,
            ITimeService time,
            ICollectionService collection,
            IRegexService regex,
            IWildcardService wildcard)
        {
            _text = text;
            _date = date;
            _time = time;
            _collection = collection;
            _regex = regex;
            _wildcard = wildcard;
        }

        // Escenario 1: Validación de cadena de conexión
        public bool IsValidConnectionString(string connStr)
        {
            var pattern = @"^(Server=|Data Source=).+;Database=|Initial Catalog=.+;";
            return _regex.IsMatch(connStr, pattern, RegexOptions.IgnoreCase);
        }

        // Escenario 2: Validación de IP (IPv4)
        public bool IsValidIPv4(string ip)
        {
            var pattern = @"^(\d{1,3}\.){3}\d{1,3}$";
            if (!_regex.IsMatch(ip, pattern)) return false;
            return ip.Split('.').All(octet => int.TryParse(octet, out var val) && val >= 0 && val <= 255);
        }

        // Escenario 3: Validación de IP (IPv6)
        public bool IsValidIPv6(string ip)
        {
            var pattern = @"^([0-9a-fA-F]{1,4}:){7}[0-9a-fA-F]{1,4}$";
            return _regex.IsMatch(ip, pattern);
        }

        // Escenario 4: Extracción de todos los emails de un texto
        public IEnumerable<string> ExtractEmails(string text)
        {
            var pattern = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";
            return _regex.ExtractAll(text, pattern);
        }

        // Escenario 5: Máscara de tarjeta de crédito (solo últimos 4)
        public string MaskCreditCard(string cardNumber)
        {
            var digits = new string(cardNumber.Where(char.IsDigit).ToArray());
            if (digits.Length < 4) return digits;
            return "****-****-****-" + digits[^4..];
        }

        // Escenario 6: Validación de fecha en formato específico
        public bool IsValidDateFormat(string dateStr, string format)
        {
            return DateTime.TryParseExact(dateStr, format,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        // Escenario 7: Cálculo de días hábiles entre dos fechas
        public int BusinessDaysBetween(DateTime start, DateTime end)
        {
            int count = 0;
            var current = start.Date.AddDays(1);
            while (current < end.Date)
            {
                if (_date.IsBusinessDay(current)) count++;
                current = current.AddDays(1);
            }
            return count;
        }

        // Escenario 8: Validación de semántica de versión (semver)
        public bool IsValidSemver(string version)
        {
            var pattern = @"^\d+\.\d+\.\d+(-[a-zA-Z0-9.]+)?(\+[a-zA-Z0-9.]+)?$";
            return _regex.IsMatch(version, pattern);
        }

        // Escenario 9: Búsqueda con wildcard en lista de archivos
        public IEnumerable<string> FindFiles(IEnumerable<string> files, string pattern)
        {
            return _wildcard.Filter(files, pattern);
        }

        // Escenario 10: Agrupación por clave con conteo
        public Dictionary<TKey, List<T>> GroupBy<T, TKey>(IEnumerable<T> items, Func<T, TKey> keySelector)
        {
            return items.GroupBy(keySelector)
                        .ToDictionary(g => g.Key, g => g.ToList());
        }

        // Escenario 11: Validación de contraseña con políticas
        public bool IsValidPassword(string password)
        {
            if (password.Length < 8) return false;
            if (!Regex.IsMatch(password, @"[A-Z]")) return false;
            if (!Regex.IsMatch(password, @"[a-z]")) return false;
            if (!Regex.IsMatch(password, @"\d")) return false;
            if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?""{}|<>_+\-=\[\];']")) return false;
            return true;
        }

        // Escenario 12: Conversión de rango de horas a formato legible
        public string FormatTimeRange(TimeSpan start, TimeSpan end)
        {
            var s = $"{start.Hours:D2}:{start.Minutes:D2}";
            var e = $"{end.Hours:D2}:{end.Minutes:D2}";
            return $"{s} - {e}";
        }

        // Escenario 13: Validación de UUID/GUID
        public bool IsValidGuid(string input)
        {
            return Guid.TryParse(input, out _);
        }

        // Escenario 14: Detección de inyección SQL básica
        public bool HasPotentialSqlInjection(string input)
        {
            var pattern = @"(DROP|DELETE|INSERT|UPDATE|SELECT|UNION|EXEC|--|;|xp_cmdshell)";
            return _regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        // Escenario 15: Validación de dominio DNS
        public bool IsValidDomain(string domain)
        {
            var pattern = @"^([a-zA-Z0-9]([a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,}$";
            return _regex.IsMatch(domain, pattern) && domain.Length <= 253;
        }
    }
}
