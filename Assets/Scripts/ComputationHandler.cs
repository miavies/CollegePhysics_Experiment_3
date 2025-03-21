using UnityEngine;
using TMPro;

public class ComputationHandler : MonoBehaviour
{
    public Clash clash;
    public int shawMass;
    public int mawMass;
    public int shawInitVelo;
    public int mawInitVelo;
    public float finalVelo;
    public float shawImp;
    public float mawImp;
    public float shawF;
    public float mawF;

    // Arrays for multiple TextMeshProUGUI elements
    public TextMeshProUGUI[] shawVelocity, shawMomentum, shawImpulse, shawForce;
    public TextMeshProUGUI[] mawVelocity, mawMomentum, mawImpulse, mawForce;
    public TextMeshProUGUI[] finalVelocity;

    void Start()
    {
        clash = GetComponent<Clash>();
    }

    void Update()
    {
        shawInitVelo = clash.shawCounter;
        mawInitVelo = -1 * clash.mawCounter;

        UpdateText(shawVelocity, shawInitVelo.ToString("F2") + " m/s");
        UpdateText(mawVelocity, mawInitVelo.ToString("F2") + " m/s");

        MomentumCalc(mawInitVelo);
        FinalVelocityCalc(mawInitVelo);
        ImpulseCalc(mawInitVelo);
        ForceCalc();
    }

    void UpdateText(TextMeshProUGUI[] textElements, string text)
    {
        foreach (var element in textElements)
        {
            if (element != null)
                element.text = text;
        }
    }

    public void MomentumCalc(int mawInitVelo)
    {
        float shawMo = shawMass * clash.shawCounter;
        UpdateText(shawMomentum, shawMo.ToString("F2") + " Ns");

        float mawMo = mawMass * mawInitVelo;
        UpdateText(mawMomentum, mawMo.ToString("F2") + " Ns");
    }

    public void FinalVelocityCalc(int mawInitVelo)
    {
        finalVelo = ((shawMass * clash.shawCounter) + (mawMass * mawInitVelo)) / (float)(shawMass + mawMass);
        UpdateText(finalVelocity, finalVelo.ToString("F2") + " m/s");
    }

    public void ImpulseCalc(int mawInitVelo)
    {
        shawImp = shawMass * (finalVelo - clash.shawCounter);
        mawImp = mawMass * (finalVelo - mawInitVelo);
        UpdateText(shawImpulse, shawImp.ToString("F2") + " Ns");
        UpdateText(mawImpulse, mawImp.ToString("F2") + " Ns");
    }

    public void ForceCalc()
    {
        shawF = shawImp / 0.5f;
        mawF = mawImp / 0.5f;
        UpdateText(shawForce, shawF.ToString("F2") + " N");
        UpdateText(mawForce, mawF.ToString("F2") + " N");
    }
}
