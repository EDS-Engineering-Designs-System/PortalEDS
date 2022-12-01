using System;

namespace bim360issues.Models
{

    public class UsuariosForge
    {
        public string Id { get; set; }
        public string? FORGE_CLIENT_ID { get; set; }
        public string? TokenInternal { get; set; }
        public string? TokenPublic { get; set; }
        public string? RefreshToken { get; set; }
        //public DateTime? ExpiresAt { get; set; }
        public string? UserId { get; set; }
        public string? EmailId { get; set; }
        public string? Name { get; set; }
        public string? Picture { get; set; }
        public DateTime DataInsercao { get; set; }

    }
}
