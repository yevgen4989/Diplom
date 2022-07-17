using System.ComponentModel.DataAnnotations;

namespace Common.Utilities;

public class FileTypeDetect
{
    private readonly List<string> photoExtensions = new() {"JPG", "JPEG", "PNG"};

    private readonly List<string> videoExtensions = new() {"MP4", "MOV", "AVI", "WMV", "WEBM"};
    
    private readonly List<string> audioExtensions = new() {"MP3", "M4A"};
    
    private readonly List<string> animationExtensions = new() {"GIF", "M4A"};
    
    private readonly List<string> voiceExtensions = new() {"OGG"};
    
    private readonly List<string> stickerExtensions = new() {"WEBP"};

    private readonly FileType _fileType;

    public FileTypeDetect(string extension)
    {
        if (photoExtensions.Contains(extension.ToUpper()))
            _fileType = FileType.Photo;
        else if (videoExtensions.Contains(extension.ToUpper()))
            _fileType = FileType.Video;
        else if (audioExtensions.Contains(extension.ToUpper()))
            _fileType = FileType.Audio;
        else if (animationExtensions.Contains(extension.ToUpper()))
            _fileType = FileType.Animation;
        else if (voiceExtensions.Contains(extension.ToUpper()))
            _fileType = FileType.Voice;
        else if (stickerExtensions.Contains(extension.ToUpper()))
            _fileType = FileType.Sticker;
        else
            _fileType = FileType.Document;
    }

    public FileType GetFileType()
    {
        return _fileType;
    }
    
    public string GetFileTypeName()
    {
        return _fileType.ToDisplay();
    }
}

public enum FileType
{
    [Display(Name = "photo")] Photo,

    [Display(Name = "video")] Video,
    
    [Display(Name = "audio")] Audio,
    
    [Display(Name = "animation")] Animation,
    
    [Display(Name = "voice")] Voice,
    
    [Display(Name = "sticker")] Sticker,

    [Display(Name = "document")] Document
}