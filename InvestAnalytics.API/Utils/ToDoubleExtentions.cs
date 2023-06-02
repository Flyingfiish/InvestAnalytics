using Tinkoff.InvestApi.V1;

namespace InvestAnalytics.API.Utils
{
    public static class ToDoubleExtentions
    {
        public static double ToDouble(this MoneyValue moneyValue)
        {
            return Convert.ToDouble($"{moneyValue.Units},{moneyValue.Nano}");
        }

        public static double ToDouble(this Quotation quotation)
        {
            return Convert.ToDouble($"{quotation.Units},{quotation.Nano}");
        }
    }
}
