using Domain.Enums;
using pix_payload_generator.net.Models.CobrancaModels;
using pix_payload_generator.net.Models.PayloadModels;

namespace Domain.Entities
{
    public class MoneyOrder : Entity
    {
        public Guid Uid { get; private set; }
        
        public string TxId { get; private set; } = string.Empty;

        public string QRCode { get; private set; } = string.Empty;

        public byte[]? QRCodeBytes { get; private set; }

        public MoneyOrderStatus Status { get; private set; }

        public decimal Value { get; private set; }

        public Guid OrderUid { get; private set; }

        private MoneyOrder()
        {
            
        }

        private MoneyOrder(decimal value, Guid orderUid)
        {
            Uid = Guid.NewGuid();
            TxId = GenerateTxId();
            QRCode = GenerateQRCode(Uid, TxId, value);
            Status = MoneyOrderStatus.PendingPayment;
            Value = value;
            OrderUid = orderUid;

        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }

        public static MoneyOrder Create(decimal value, Guid orderUid) => new MoneyOrder(value, orderUid); 

        public void SetQrCodeBytes(byte[] bytes) => QRCodeBytes = bytes;

        public void SetStatus(MoneyOrderStatus status) => Status = status;

        private static string GenerateTxId()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            
            //TXID deve ter no mínimo 26 caracteres e no máximo 35 caracteres.
            var txId = new char[35];
            var random = new Random();

            for (int i = 0; i < txId.Length; i++)
            {
                txId[i] = chars[random.Next(chars.Length)];
            }

            return new String(txId);
        }

        private static string GenerateQRCode(Guid uid, string txId, decimal value)
        {
            var charge = new Cobranca(_chave: uid.ToString())
            {
                SolicitacaoPagador = $"Pagamento do Pedido {uid}",
                Valor = new Valor
                {
                    Original = value.ToString()
                }
            };

            var payload = charge.ToPayload(txId, new Merchant("QuickBite", "São Paulo"));
            return payload.GenerateStringToQrCode();
        }
    }
}
