# Anleitung zur Konfiguration des Wellensystems

## Übersicht
Diese Anleitung beschreibt, wie Sie das Wellensystem in Ihrem Spiel konfigurieren können. Das System ist dafür ausgelegt, Gegner in verschiedenen Mustern und Schwierigkeitsgraden zu spawnen, um ein dynamisches Spielerlebnis zu schaffen.

## Konfiguration der Wellen

### Einstellen der Grundparameter
- **Wave Start Mode**: Bestimmen ob die nächste Welle nach Zeit oder nach dem alle Gegner getötet wurden, startet.
- **Start Delay**: Bestimmen Sie die Zeitverzögerung, bevor eine Welle beginnt.
- **Time Between Waves**: Legen Sie fest, wie lange eine Welle dauert, bevor die nächste beginnt.

### Definition der Gegnerwellen
- Jede Welle besteht aus mehreren Gruppen von Gegnern.
- Für jede Gruppe können Sie folgendes festlegen:
  - **Gegnertypen**: Wählen Sie aus verschiedenen Gegner-Prefabs.
  - **Anzahl der Gegner**: Legen Sie fest, wie viele Gegner jedes Typs in der Gruppe erscheinen.
  - **Spawn-Punkt-Index**: Bestimmen Sie, an welchem Spawn-Punkt die Gegner erscheinen, oder wählen Sie `-1` für zufällige Spawn-Punkte.
  - **Health**: Bestimmen Sie, wie viel Leben die Gegner haben.
  - **Spawn Time**: Bestimmen Sie, nach wieviel Sekunden nach Wave start der Gegner spawnen soll.

### Konfiguration der Spawn-Punkte
- Definieren Sie in Ihrer Spielwelt verschiedene Orte, an denen Gegner spawnen können.
- Weisen Sie jedem Spawn-Punkt einen eindeutigen Index zu.

## Implementierung im Unity-Editor

### Einrichten der Wellen
- Erstellen Sie ein `WaveManager`-GameObject in Ihrer Szene.
- Fügen Sie Ihrem `WaveManager`-GameObject das Wellenverwaltungsskript hinzu.
- Konfigurieren Sie im Inspektor die Wellen und deren Parameter.

### Zuweisung der Gegner-Prefabs
- Stellen Sie sicher, dass Sie für jeden Gegnertyp ein Prefab in Ihrem Projekt haben.
- Weisen Sie diese Prefabs den entsprechenden Gegnertypen in den Welleneinstellungen zu.

### Festlegen der Spawn-Punkte
- Platzieren Sie in Ihrer Szene GameObjects, die als Spawn-Punkte dienen.
- Weisen Sie diese Spawn-Punkte im `WaveManager`-Skript zu.
