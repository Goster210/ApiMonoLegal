using MongoDB.Bson.Serialization.Attributes;

namespace ApiMonoLegal.Models
{
    public class Factura
    {
        // Variables de la entidad
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string codigoFactura { get; private set; }
        [BsonElement("cliente")]
        public string cliente { get; private set; }
        [BsonElement("correo")]
        public string correo { get; private set; }
        [BsonElement("ciudad")]
        public string ciudad { get; private set; }
        [BsonElement("nit")]
        public long nit { get; private set; }
        [BsonElement("totalFactura")]
        public double totalFactura { get; private set; }
        [BsonElement("subTotal")]
        public double subTotal { get; private set; }
        [BsonElement("iva")]
        public double iva { get; private set; }
        [BsonElement("retencion")]
        public double retencion { get; private set; }
        [BsonElement("fechaCreacion")]
        public DateTime? fechaCreacion { get; private set; }
        [BsonElement("estado")]
        // como tengo que modificar este dato desde otras clases el setter tiene que ser publico
        public string estado { get; set; }
        [BsonElement("pagada")]
        public bool pagada { get; private set; }
        [BsonElement("fechaPago")]
        public DateTime? fechaPago { get; private set; }

        private Factura()
        {
            // Constructor privado vacío para uso interno.
        }

        // Constructor para instanciar la entidad en otras clases

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
