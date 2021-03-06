﻿
using AutoMapper;
using System.Collections.Generic;
using Wiki.Models.Biz.DTO;
using Wiki.Models.Biz.Interfaces;

namespace Wiki.Models.Biz {
    public class ArticleManager {

        private readonly IArticleRepository _articleRepository; // readonly: seulement le ctor peut modifier sa valeur

        public IList<Article> lstArticles { get; set; }
        public IList<string> lstTitres { get; set; }

        public ArticleManager(IArticleRepository articleRepository) {
            this._articleRepository = articleRepository;
            this.lstArticles = this.GetArticles();
            //this.lstTitres = this.GetTitres();
        }

        public void UpdateLists() {
            this.lstArticles = this.GetArticles();
            //this.lstTitres = this.GetTitres();
        }

        public int Add(ArticleDTO aDto) {

            int result = -1;
            try {
                result = _articleRepository.Add(aDto);

                if (result == 1)
                    this.UpdateLists();

            } catch (System.Exception) {
                throw;
            }

            return result;
        }

        public int Update(ArticleDTO aDto) {

            int result = -1;
            try {
                result = _articleRepository.Update(aDto);

                if (result == 1)
                    this.UpdateLists();

            } catch (System.Exception) {
                throw;
            }

            return result;
        }

        public int Delete(string titre) {
            int result = -1;
            try {
                result = _articleRepository.Delete(titre);

                if (result == 1)
                    this.UpdateLists();

            } catch (System.Exception) {
                throw;
            }

            return result;
        }

        public Article Find(string titre) {

            try {
                ArticleDTO aDto = _articleRepository.Find(titre);
                Article a = Mapper.Map<ArticleDTO, Article>(aDto);

                return a;

            } catch (System.Exception) {
                throw;
            }

        }

        public IList<Article> GetArticles() {
           
            try {
                IList<ArticleDTO> lstArticlesDto = _articleRepository.GetArticles();
                IList<Article> lstArticles = Mapper.Map<IList<ArticleDTO>, IList<Article>>(lstArticlesDto);

                return lstArticles;
            } catch (System.Exception) {
                throw;
            }

        }

        public IList<string> GetTitres() {

            try {
                var lstTitresDto = _articleRepository.GetTitres();
                var lstTitres = new List<string>();
                foreach (var titreDto in lstTitresDto)
                    lstTitres.Add(titreDto);

                return lstTitres;
            } catch (System.Exception) {
                throw;
            }

        }

    }
}