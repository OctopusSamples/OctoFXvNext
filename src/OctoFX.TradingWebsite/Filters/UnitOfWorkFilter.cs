using Microsoft.AspNet.Mvc.Filters;


namespace OctoFX.TradingWebsite.Filters
{
    public class UnitOfWorkFilter : IActionFilter
    {
		//readonly ISession session;
		public UnitOfWorkFilter()
		{
			//this.session = session;
		}
		
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //filterContext.HttpContext.Items["ThisTransaction"] = session.BeginTransaction();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception == null)
            {
                //  var transaction = (ITransaction)filterContext.HttpContext.Items["ThisTransaction"];
                //  session.Flush();
                //  transaction.Commit();                
            }
        }
    }
}