using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoplayasiaBlazor.Models
{
    public class SurveyAnswerModel
	{
		public int Id { get; set; }
		public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
}
