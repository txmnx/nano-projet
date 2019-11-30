/**
 * Implement this interface if you want to make something on every input beat.
 * In order to be detected your class must be stored with InputTranslator.RegisterOnInputBeatElement()
 */
public interface OnInputBeatElement
{
    void OnEnterInputBeat();
    void OnInputBeat();
}