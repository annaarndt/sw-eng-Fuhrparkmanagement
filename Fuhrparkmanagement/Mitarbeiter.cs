//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fuhrparkmanagement
{
    using System;
    using System.Collections.Generic;
    
    public partial class Mitarbeiter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Mitarbeiter()
        {
            this.LegtBuchungAn = new HashSet<Buchung>();
        }
    
        public int Id { get; set; }
        public string Personalnummer { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public bool Führerschein { get; set; }
        public string Führerscheinnummer { get; set; }
        public bool IstAdmin { get; set; }
    
        public virtual Fahrzeug Hauptnutzerfahrzeug { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Buchung> LegtBuchungAn { get; set; }
    }
}
