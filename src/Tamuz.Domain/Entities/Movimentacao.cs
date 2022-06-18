namespace Tamuz.Domain.Entities
{
    public class Movimentacao
    {
        public int Id { get; set; }
        public string IspbCreditante { get; set; }
        public string AgenciaCreditante { get; set; }
        public string ContaCreditante { get; set; }
        public string ChaveCreditante { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataOperacao { get; set; }
        public DateTime DataContabil { get; set; }
        public string IspbDebitante { get; set; }
        public string AgenciaDebitante { get; set; }
        public string ContaDebitante { get; set; }
        public string ChaveDebitante { get; set; }
        public TipoMovimentacao Tipo { get; set; }
    }
    public enum TipoMovimentacao
    {
        Ted = 1,
        Tef = 2,
        Pix = 3,
        Cheque = 4
    }
}
