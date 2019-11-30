/**
 * Implement this interface if you want to make something on every action beat.
 * In order to be detected your class must be stored with InputTranslator.RegisterOnActionBeatElement()
 */
public interface OnActionBeatElement
{
    void OnEnterActionBeat();
    void OnActionBeat();
}