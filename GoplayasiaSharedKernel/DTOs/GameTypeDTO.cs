using GoplayasiaBlazor.Dtos.DTOIn;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.DTOs
{
    public class GameTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? CorporateCommission { get; set; }
        public decimal? MasterAgentCommission { get; set; }
        public decimal? AgentCommission { get; set; }

        public GameSettingDTO GameSetting { get; set; }
        public GameChipDTO GameChip { get; set; }
        public GameChipDTO FixedChip { get; set; }
        public GameChipDTO RunningChip { get; set; }

        public GameChipDTO F2Chip { get; set; }
        public GameChipDTO F3Chip { get; set; }
        public GameChipDTO A4Chip { get; set; }
    }
}
