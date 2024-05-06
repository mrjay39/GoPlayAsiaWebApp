using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Dtos.DTOIn.Profile
{
    public class UploadDTO
    {
        public long Id { get; set; }
        public int Type { get; set; }
        public string Extension { get; set; }
        public string MetadataName { get; set; }
        public string OriginalFileName { get; set; }
        public string Path { get; set; }
        public DateTime DateUploaded { get; set; }

        public string ContentType { get; set; }
        public byte[] FileBytes { get; set; }
        public string SAS { get; set; }
        public string PathWithSAS { get; set; }
    }
}
