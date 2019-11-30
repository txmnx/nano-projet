/**
 * Implement this interface if you want to make something on every beat.
 * In order to be detected your class must be stored with BeatManager.RegisterOnBeatElement()
 */
public interface OnBeatElement
{
    void OnBeat();
}