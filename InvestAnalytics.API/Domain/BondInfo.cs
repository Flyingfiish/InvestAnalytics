using Tinkoff.InvestApi.V1;

namespace InvestAnalytics.API.Domain
{
    public class BondInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UId { get; set; }
        public string Figi { get; set; }
        public string Ticker { get; set; }
        public string Isin { get; set; }
        public string ClassCode { get; set; }
        
        public int Lot { get; set; }
        public string Currency { get; set; }
        public string Exchange { get; set; }
        public int CouponQuantityPerYear { get; set; }
        /// <summary>
        /// Дата погашения
        /// </summary>
        public DateTime MaturityDate { get; set; }
        public double Nominal { get; set; }
        public double InitialNominal { get; set; }
        /// <summary>
        /// Дата выпуска облигации
        /// </summary>
        public DateTime StateRegDate { get; set; }
        /// <summary>
        /// Дата размещения
        /// </summary>
        public DateTime PlacementDate { get; set; }
        public double PlacementPrice { get; set; }
        public string CountryOfRiskCode { get; set; }
        public string CountryOfRiskName { get; set; }
        public string Sector { get; set; }
        /// <summary>
        /// Размер выпуска
        /// </summary>
        public Int64 IssueDate { get; set; }
        
        /// <summary>
        /// Признак внебиржевой ценной бумаги
        /// </summary>
        public bool Otc { get; set; }
        public bool BuyAvailable { get; set; }
        public bool SellAvailable { get; set; }
        public bool FloatingCoupon { get; set; }
        /// <summary>
        /// Признак бессрочной облигации.
        /// </summary>
        public bool Perpetual { get; set; }
        public bool Amortization { get; set; }
        public bool ForQualInvestor { get; set; }
        
        public double MinPriceIncrement { get; set; }
        public RiskLevel RiskLevel { get; set; }
    }
}
