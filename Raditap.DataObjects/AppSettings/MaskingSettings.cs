namespace Raditap.DataObjects.AppSettings
{
    public class MaskingSettings
    {
        public string[] CitizenIdFields { get; set; }
        public char CitizenMaskCharacter { get; set; }
        public string[] PassportNumberFields { get; set; }
        public char PassportNumberMaskCharacter { get; set; }
        public string[] MaskLengthFields { get; set; }
        public int MaskLength { get; set; }
        public string[] PasswordFields { get; set; }
        public string PasswordMaskCharacter { get; set; }
        public char MaskPanCharacter { get; set; }
    }
}
