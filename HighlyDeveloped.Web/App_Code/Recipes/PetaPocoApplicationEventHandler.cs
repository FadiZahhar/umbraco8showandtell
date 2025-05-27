//using Recipes.Models.pocos;

//using Spring.Context;
//using Umbraco.Core;
//using Umbraco.Core.Persistence;
//using Umbraco.Web;

//namespace Recipes
//{
//    public class PetaPocoApplicationEventHandler : IUserComposer
//    {
//        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
//        {
//            var ctx = applicationContext.DatabaseContext;
//            var db = new DatabaseSchemaHelper(ctx.Database, applicationContext.ProfilingLogger.Logger, ctx.SqlSyntax);

//            if (!db.TableExist("CMSRecipes"))
//            {
//                db.CreateTable<recipe>(false);
//            }
//        }
//    }
//}