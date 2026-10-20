# BitirmeProjesi

Tiny Swords asset'leriyle yapılan 2D RTS oyunu (bitirme projesi).

## Kurulum

1. **Unity 6000.4.2f1** sürümünü Unity Hub'dan kur (farklı sürüm kullanmayın, proje dosyaları değişir).
2. Repoyu klonla:
   ```bash
   git clone <repo-adresi>
   ```
3. Unity Hub → **Add** → klonlanan klasörü seç ve aç. İlk açılış birkaç dakika sürer (`Library` klasörü üretilir).
4. Unity'de üst menüden **RTS → Harita Oluştur**'a tıkla. `Assets/Scenes/RTSMap.unity` oluşur ve açılır.
5. **Play**'e bas.

Harita sahnesi büyük (~75 MB) olduğu için repoda tutulmuyor. Seed sabit olduğu için herkesin
bilgisayarında aynı harita oluşur. Haritayı değiştirmek için `Assets/Scripts/Editor/RTSMapGenerator.cs`
içindeki `MapSeed` değerini değiştirip commit'leyin.

## Kontroller

- **WASD / ok tuşları**: kamerayı gezdir
- **Shift**: hızlı gezinme
- **Fare tekerleği**: yakınlaştır / uzaklaştır

## Çalışma düzeni

- Çalışmaya başlamadan önce: `git pull`
- Her iş için ayrı branch açın, bitince GitHub'da Pull Request ile `main`'e birleştirin.
- **Aynı sahneyi (.unity) veya prefab'ı aynı anda iki kişi düzenlemesin**; Unity sahne dosyalarında
  çakışma çözmek çok zordur. Kim hangi sahnede çalışıyorsa önceden haber versin.
