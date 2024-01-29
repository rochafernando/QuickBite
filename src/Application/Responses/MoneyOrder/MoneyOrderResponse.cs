using Domain.Enums;
using Domain.Interfaces.CQS;

namespace Application.Responses.MoneyOrder
{
    public class MoneyOrderResponse : IResult
    {
        /// <summary>
        /// Uid da ordem de pagamento
        /// </summary>
        public Guid Uid { get; set; }

        /// <summary>
        /// TxId da ordem de pagamento
        /// </summary>
        public string TxId { get; set; } = string.Empty;

        /// <summary>
        /// QRCode da ordem de pagamento
        /// </summary>
        public string QRCode { get; set; } = string.Empty;

        /// <summary>
        /// Bytes do QRCode
        /// </summary>
        public byte[]? QRCodeBytes { get; set; }

        /// <summary>
        /// Status da ordem de pagamento
        /// </summary>
        public MoneyOrderStatus Status { get; set; }

        /// <summary>
        /// Valor da ordem de pagamento.
        /// </summary>
        public decimal Value { get; set; }
    }
}
