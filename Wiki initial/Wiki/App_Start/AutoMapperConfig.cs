using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wiki.Models.Biz;
using Wiki.Models.Biz.DTO;
using Wiki.Models.ViewModels;

namespace Wiki {
    // https://github.com/AutoMapper/AutoMapper/wiki/Static-and-Instance-API             
    // https://stackoverflow.com/questions/42508231/automapper-5-2-how-to-configure
    public class AutoMapperConfig {
        // 
        public static void RegisterMappings() { 
                                                
            AutoMapper.Mapper.Initialize(cfg => {             //           (getArt)
                cfg.CreateMap<Article, ArticleDTO>();         // ┌--> aDto DAL -> BIZ(aDto)
                cfg.CreateMap<ArticleDTO, Article>();         // |                a BIZ ----> a CTL
                cfg.CreateMap<Article, ArticleViewModel>();   // db(a)                        aVM CTL -> VUE (aVM)
                cfg.CreateMap<ArticleViewModel, ArticleDTO>();// |                         BIZ(aDto)  <- CTL (aVM)
                                                              // └-----<----- DAL(aDto) <- BIZ(aDto)           
                                                              //             (add/upd/del)
                // cfg.CreateMap<type SOURCE, type DESTINATION>();
            });

            // Exemples utilisés dans le contrôleur :
            //
            //Ex1 pour mapper:  
            // Article article = _articleManager.lstArticles.FirstOrDefault(p => p.Titre.Equals(titre));
            // ArticleViewModel model = Mapper.Map<Article, ArticleViewModel>(article); // conversion d'une entité Article en ArticleViewModel
            //return View(model)

            //Ex2 pour mapper:
            // IList<Article> lArt = _articleManager.lstArticles;
            // IList<ArticleViewModel> model = Mapper.Map<IList<Article>, IList<ArticleViewModel>>(lArt);
            // return PartialView(model);

        }

    }
}