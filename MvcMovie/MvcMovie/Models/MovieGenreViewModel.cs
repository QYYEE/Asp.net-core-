using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Models
{
    public class MovieGenreViewModel
    {
        /// <summary>
        /// 电影列表
        /// </summary>
        public List<Movice> Movices { get; set; }
        /// <summary>
        /// 包含流派列表的A，这允许用户从列表中选择类型
        /// </summary>
        public SelectList Genres { get; set; }
        /// <summary>
        /// 包含所选的流派
        /// </summary>
        public string MovieGenre { get; set; }
        /// <summary>
        /// 包含用户在搜索文本框中输入的文本
        /// </summary>
        public string SearchString { get; set; }

    }
}
