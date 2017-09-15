using System.Collections.Generic;
using Wiki.Models.Biz.DTO;

namespace Wiki.Models.Biz.Interfaces {
    public interface IArticleRepository {

        int Add(ArticleDTO a);
        int Update(ArticleDTO a);
        int Delete(string titre);
        ArticleDTO Find(string titre);
        IList<ArticleDTO> GetArticles();
        IList<string> GetTitres();
    }
}