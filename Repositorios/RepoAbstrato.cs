namespace bim360issues.Repositorios
{
    public abstract class RepoAbstrato
    {
        protected string _conectionString;

        public RepoAbstrato(string conectionString)
        {
            _conectionString = conectionString;
        }
    }
}
