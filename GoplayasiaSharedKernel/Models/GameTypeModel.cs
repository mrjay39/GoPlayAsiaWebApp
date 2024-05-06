using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Models
{
    public class GameTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? CorporateCommission { get; set; }
        public decimal? MasterAgentCommission { get; set; }
        public decimal? AgentCommission { get; set; }

        public GameSettingModel GameSetting { get; set; }
        public GameChipModel GameChip { get; set; }

        public GameChipModel FixedChip { get; set; }
        public GameChipModel RunningChip { get; set; }

        public GameChipModel F2Chip { get; set; }
        public GameChipModel F3Chip { get; set; }
        public GameChipModel A4Chip { get; set; }
    }
}
