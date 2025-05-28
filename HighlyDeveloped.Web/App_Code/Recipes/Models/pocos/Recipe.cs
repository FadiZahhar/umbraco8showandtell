using NPoco;
using System;
using System.Collections.Generic;
using System.Text;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Recipes.Models.pocos
{

    [TableName("CMSRecipes")]
    [PrimaryKey("RecipeId", AutoIncrement = true)]
    [ExplicitColumns]

    public class recipe
    {

        [Column("RecipeId")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int RecipeId { get; set; }

        [Column("RecipeName")]
        [Length(250)]
        public string RecipeName { get; set; }

        [Column("Description")]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        public string Description { get; set; }

        [Column("Directions")]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        public string Directions { get; set; }

        [Column("ServingSize")]
        [NullSetting(NullSetting = NullSettings.Null)]
        [Length(100)]
        public string ServingSize { get; set; }

        [Column("DateSubmitted")]
        public DateTime DateSubmitted { get; set; }

        [Column("DateApproved")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public DateTime DateApproved { get; set; }

        [Column("UserId")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public int UserId { get; set; }

        [Column("CategoryID")]
        public int CategoryID { get; set; }

    }
}