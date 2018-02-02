[How To Configure A Self Referencing Entity In Code First](https://roland.kierkels.net/c-asp-net/how-to-configure-a-self-referencing-entity-in-code-first/)

[Many To Many Relationships](https://adrianscorner.wordpress.com/2014/04/04/designing-a-many-to-many-relationship-with-additional-fields-using-entity-framework/)

[Many To Many Relationships With Additional Fields In Association Table](https://stackoverflow.com/questions/7050404/create-code-first-many-to-many-with-additional-fields-in-association-table)

[]()
[]()
[]()
[]()


#Self Relationship

```cs
    public partial class MenuEntity
    {
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
		
        [Column("description")]
        public string Description { get; set; }

        [Column("link")]
        public string Link { get; set; }

        [Column("sort_position")]
        public int SortPosition { get; set; }

        [Column("status")]
        public GenericStatus Status { get; set; }

        public int? ancestor_id { get; set; }

        public virtual MenuEntity AncestorMenu { get; set; }
        public virtual ICollection<MenuEntity> DescendingMenus { get; set; }
     }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MenuEntity>(entity =>
        {
            entity.HasKey(e => e.Id);

            /*entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar");*/

            entity
				.HasOne(d => d.AncestorMenu)
				.WithMany(p => p.DescendingMenus)
				.HasForeignKey(d => d.ancestor_id);
        });
    }

```	

#Many To Many Relationship With Extra Columns

```cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization; 

	
.
.
.
	
[Table("permission", Schema = "my_schema")]
[DataContract]
public class PermissionEntity
{
	[Key]
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Int32 PermissionId { get; set; }

	[Column("description")]
	public string Description { get; set; }

	[Column("status")]
	public GenericStatus Status { get; set; }

	public virtual IList<ProfilePermissionEntity> ProfilePermissions { get; set; }
}
```   
	
.
.
.

```cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization; 
	
.
.
.

[Table("profile", Schema = "my_schema")]
[DataContract]
public class ProfileEntity
{
	[Key]
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Int32 ProfileId { get; set; }

	[Column("title")]
	public string Title { get; set; }

	[Column("code")]
	public string Code { get; set; }

	[Column("last_update")]
	public DateTime LastUpdate { get; set; }

	[Column("status")]
	public GenericStatus Status { get; set; }

	public virtual IList<ProfilePermissionEntity> ProfilePermissions { get; set; }

}
```   
	
.
.
.

```cs	
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
	
.
.
.
	
[Table("profile_permission", Schema = "my_schema")]
[DataContract]
public class ProfilePermissionEntity
{
	[Key, Column(name: "profile_id", Order = 0)]
	public int ProfileId { get; set; }

	[Key, Column(name: "permission_id", Order = 1)]
	public int PermissionId { get; set; }

	public virtual ProfileEntity Profile { get; set; }
	public virtual PermissionEntity Permission { get; set; }

	[Column("status")]
	public GenericStatus Status { get; set; }
}

//SEE: https://stackoverflow.com/questions/7050404/create-code-first-many-to-many-with-additional-fields-in-association-table
```   
	
.
.
.

```cs
public enum GenericStatus
{
	[Display(Name ="Inativo")]
	INACTIVE,

	[Display(Name = "Ativo")]
	ACTIVE
}
```
