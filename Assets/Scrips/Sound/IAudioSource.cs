

public interface IAudioSource
{
    void Stop();
    void FadeOut(float fateTime);
    bool IsLooping { get; set; }
    bool IsPlaying { get; }
}
