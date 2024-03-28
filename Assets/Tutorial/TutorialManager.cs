using System.Collections;
using UnityEngine;
using TMPro; // Für TextMeshPro



public class TutorialManager : MonoBehaviour
{
    private bool introCompleted = false;
    private bool hasInvinc = false;
    private bool isSword = false;




    public enum TutorialStep
    {
        Intro,
        NormalGun,
        DefeatEnemies,
        UseWaterGun,
        RefillWaterGun,
        UseSword,
        SwordAbillity,
        SpwanBarrels,
        OpenChest,
        UseInvincevle,
        UseShop,
        BuildShip,
        End,
        Completed
    }

    public TutorialStep currentStep = TutorialStep.Intro;

    // Verweise auf UI-Elemente und andere Skripte
    public TMP_Text tutorialText;

    void Start()
    {
        UpdateTutorialStep(TutorialStep.Intro);
    }

    void Update()
    {
        CheckProgress();
    }

    void CheckProgress()
    {
        switch (currentStep)
        {
            case TutorialStep.Intro:
                // Starte die Coroutine nur, wenn sie noch nicht läuft
                if (!introCompleted) // Stelle sicher, dass du eine bool Variable `introCompleted` in deiner Klasse hast
                {
                    StartCoroutine(GoToNextStepAfterDelay(TutorialStep.NormalGun, 3f));
                    introCompleted = true; // Verhindere, dass die Coroutine mehrfach gestartet wird
                }
                break;
            case TutorialStep.NormalGun:
                // Überprüfe, ob RechtsKlick gedrückt wurde
                if (Input.GetMouseButtonDown(0)) // 0 = linke Maustaste, 1 = rechte Maustaste, 2 = mittlere Maustaste
                {
                    UpdateTutorialStep(TutorialStep.DefeatEnemies);
                }
                break;
            case TutorialStep.DefeatEnemies:
                // Überprüfe, ob alle Gegner besiegt wurden
                if (false/* Gegner besiegt */)
                {
                    UpdateTutorialStep(TutorialStep.UseWaterGun);
                }
                break;
            // Implementiere weitere Fälle für jeden Schritt
            case TutorialStep.UseWaterGun:
                // Überprüfe, ob RechtsKlick gedrückt wurde
                if (Input.GetMouseButtonDown(1)) // 0 = linke Maustaste, 1 = rechte Maustaste, 2 = mittlere Maustaste
                {
                    UpdateTutorialStep(TutorialStep.RefillWaterGun);
                }
                break;
            case TutorialStep.RefillWaterGun:
                
                if (Input.GetMouseButtonDown(1))
                {
                    StartCoroutine(GoToNextStepAfterDelay(TutorialStep.UseSword, 2f));
                   
                }
                break;
            case TutorialStep.UseSword:

                if (isSword)
                {
                    StartCoroutine(GoToNextStepAfterDelay(TutorialStep.SpwanBarrels, 2f));
                }
                break;
            // Implementiere weitere Fälle für jeden Schritt
            case TutorialStep.SwordAbillity:

                if (Input.GetMouseButtonDown(1))
                {
                    StartCoroutine(GoToNextStepAfterDelay(TutorialStep.SpwanBarrels, 1f));
                }
                break;
            case TutorialStep.OpenChest:

               
                StartCoroutine(GoToNextStepAfterDelay(TutorialStep.UseShop, 3f));
                
                break;
            case TutorialStep.UseInvincevle:

                if (Input.GetKeyDown(KeyCode.I))
                {
                    StartCoroutine(GoToNextStepAfterDelay(TutorialStep.UseShop, 1f));
                }
                break;
            case TutorialStep.UseShop:

                if (Input.GetKeyDown(KeyCode.F))
                {
                    StartCoroutine(GoToNextStepAfterDelay(TutorialStep.BuildShip, 1f));
                }
                break;
            case TutorialStep.BuildShip:

                if (Input.GetKeyDown(KeyCode.F))
                {
                    StartCoroutine(GoToNextStepAfterDelay(TutorialStep.End, 1f));
                }
                break;

                // Implementiere weitere Fälle für jeden Schritt
        }
    }

    void UpdateTutorialStep(TutorialStep newStep)
    {
        currentStep = newStep;
        // Aktualisiere das Tutorial-Textfeld oder führe andere Aktionen für den neuen Schritt aus
        switch (newStep)
        {
            case TutorialStep.Intro:
                tutorialText.text = "Welcome to Aqua Defender";
                break;
            case TutorialStep.NormalGun:
                tutorialText.text = "Tip: Use left-click to shoot with your weapon.";
                break;
            case TutorialStep.DefeatEnemies:
                tutorialText.text = "Defeat all enemies!";
                break;
            case TutorialStep.UseWaterGun:
                tutorialText.text = "Tip: Press right-click to use the water cannon.";
                break;
            case TutorialStep.RefillWaterGun:
                tutorialText.text = "Tip: Walk through water or through water drops left by enemies to refill your water cannon.";
                break;
            case TutorialStep.UseSword:
                tutorialText.text = "Tip: Out of ammo? Use the weapon on the left mouse button or switch to the sword with the 'E' key.";
                break;
            case TutorialStep.SwordAbillity:
                tutorialText.text = "Tip: Use the sword and press right-click to freeze enemies.";
                break;
            case TutorialStep.SpwanBarrels:
                tutorialText.text = "Tip: Pick up barrels dropped by enemies and place them with the SPACE bar. Hit or shoot them to make them explode.";
                break;
            case TutorialStep.OpenChest:
                tutorialText.text = "Tip: Look for a chest and open it with a left-click. But be careful – some might fight back!";
                break;
            case TutorialStep.UseInvincevle:
                tutorialText.text = "Tip: Press the 'I' key to activate invisibility.";
                break;
            case TutorialStep.UseShop:
                tutorialText.text = "Tip: Go to the white man at the spawn point and open the shop with the 'F' key.";
                break;
            case TutorialStep.BuildShip:
                tutorialText.text = "Goal: Use the coins from enemies to buy all ship parts and escape from the island.";
                break;
            case TutorialStep.End:
                tutorialText.text = "Have fun playing!";
                break;

                // Setze dies für jeden Schritt fort
        }
    }
    public void SetTutorialText(string text)
    {
        tutorialText.text = text;
    }

    public void HideTutorialText()
    {
        tutorialText.gameObject.SetActive(false);
    }

    public void ShowTutorialText()
    {
        tutorialText.gameObject.SetActive(true);
    }
    private void OnEnable()
    {
        WaveManager.OnWaveCompleted += HandleWaveCompleted;
        BarrelSpawner.OnBarrelSpawned += HandleBarrelSpawned; // Abonnieren des BarrelSpawn-Events
        Chest.OnChestOpened += HandleChestOpened;
        PlayerMovementController.OnSwordSwitched += HandleSwordSwitched;
        PlayerMovementController.OnGunSwitched += HandleGunSwitched;
    }

    private void OnDisable()
    {
        WaveManager.OnWaveCompleted -= HandleWaveCompleted;
        BarrelSpawner.OnBarrelSpawned -= HandleBarrelSpawned; // Event-Abonnement entfernen
        Chest.OnChestOpened -= HandleChestOpened;
        PlayerMovementController.OnSwordSwitched -= HandleSwordSwitched;
        PlayerMovementController.OnGunSwitched -= HandleGunSwitched;
    }

    IEnumerator GoToNextStepAfterDelay(TutorialStep nextStep, float delay)
    {
        yield return new WaitForSeconds(delay); // Warte für die angegebene Verzögerung
        UpdateTutorialStep(nextStep); // Aktualisiere dann zum nächsten Tutorial-Schritt
    }
    private void HandleWaveCompleted()
    {
        if (currentStep == TutorialStep.DefeatEnemies|| currentStep == TutorialStep.NormalGun)
        {
            // Gehe zum nächsten Schritt, wenn die Gegnerwelle abgeschlossen ist
            UpdateTutorialStep(TutorialStep.UseWaterGun); // Implementiere diese Methode entsprechend deiner Logik
        }
    }
    private void HandleBarrelSpawned()
    {
        // Logik, um zum nächsten Tutorial-Schritt überzugehen, nachdem ein Fass gespawnt wurde
        // Beispiel:
        
        if (currentStep == TutorialStep.SpwanBarrels)
        {
            UpdateTutorialStep(TutorialStep.OpenChest); // Beispiel für eine Verzögerung
        }
    }
    private void HandleSwordSwitched()
    {
        isSword = true;
    }

    private void HandleGunSwitched()
    {
        isSword = false;
    }


    private void HandleChestOpened()
    {
        hasInvinc = true;
    }
}
