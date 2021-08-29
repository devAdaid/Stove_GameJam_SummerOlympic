using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundUtil : MonoBehaviour
{
    public SfxType SfxType;

    public void PlaySound()
    {
        AudioManager.I.PlaySfx(SfxType);
    }
}
