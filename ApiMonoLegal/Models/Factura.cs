using MongoDB.Bson.Serialization.Attributes;

namespace ApiMonoLegal.Models
{
    public class Factura
    {
        // Variables de la entidad
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string codigoFactura { get;  set; }
        [BsonElement("cliente")]
        public string cliente { get;  set; }
        [BsonElement("correo")]
        public string correo { get;  set; }
        [BsonElement("ciudad")]
        public string ciudad { get;  set; }
        [BsonElement("nit")]
        public long nit { get;  set; }
        [BsonElement("totalFactura")]
        public double totalFactura { get;  set; }
        [BsonElement("subTotal")]
        public double subTotal { get;  set; }
        [BsonElement("iva")]
        public double iva { get;  set; }
        [BsonElement("retencion")]
        public double retencion { get;  set; }
        [BsonElement("fechaCreacion")]
        public DateTime? fechaCreacion { get;  set; }
        [BsonElement("estado")]
        public string estado { get;  set; }
        [BsonElement("pagada")]
        public bool pagada { get;  set; }
        [BsonElement("fechaPago")]
        public DateTime? fechaPago { get;  set; }

        // Constructor
        public Factura(string codigoFactura, string cliente, string correo, string ciudad, long nit, double totalFactura, double subTotal, double iva, double retencion, DateTime? fechaCreacion, string estado, bool pagada, DateTime? fechaPago)
        {
            this.codigoFactura = codigoFactura;
            this.cliente = cliente;
            this.correo = correo;
            this.ciudad = ciudad;
            this.nit = nit;
            this.totalFactura = totalFactura;
            this.subTotal = subTotal;
            this.iva = iva;
            this.retencion = retencion;
            this.fechaCreacion = fechaCreacion;
            this.estado = estado;
            this.pagada = pagada;
            this.fechaPago = fechaPago;
        }

    }
}
