using System;
using Newtonsoft.Json;

namespace Raditap.BusinessLogic.ApiDataObjects
{
    public class PageInfo
    {
        public PageInfo(int totalRecord, int pageSize)
        {
            TotalRecord = totalRecord;

            if (totalRecord == 0 || pageSize == 0)
            {
                TotalPage = 1;
            }
            else
            {
                var result = (int)Math.Ceiling((double)totalRecord / pageSize);
                TotalPage = result == 0 ? 1 : result;
            }
        }

        [JsonProperty("totalPage")]
        public int TotalPage { get; set; }

        [JsonProperty("totalRecord")]
        public int TotalRecord { get; set; }
    }
}
