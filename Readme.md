# Modular Template

ProjectGenerator.ps1 yardımı ile template'teki proje örneği, yine bu powershell dosyasının içindeki yeni proje adı olarak ne verildiyse o olacak şekilde oluşturulur. Oluşturulan projede "CoreModule" yerleşik olarak gelir ve temel şablon aktarılır.

ModuleGenerator.ps1 yardımı ile template'teki module örneği, yine bu powershell dosyasının içindeki yeni proje adı olarak tanımlı değişkendeki değere göre yeni bir modül şablonu oluşturur.

Birde bin, obj dosyalarını hızlı şekilde projelerden silebilmek için kolaylık olsun diye bin_obj_remove.ps1 powershell script'i ekledim.

Genel olarak bir web uygulaması için temel ihtiyaçları giderecek bir mimari kurgu hedeflenmiştir ve şimdilik emekleme aşamasındadır :). Komut ve sorgu sorumluluklarının ayrımı olarak dilimize çevrilen ve orjinal dilindeki kısaltması CQRS olan tasarım deseni benimsenmiştir.

Aslında şablonun adını düşünürken bir ara "OrtayaKarisik" ismini düşündüğümde oldu :) zira açık kaynak kodlu proje şablonlarının bir çoğundan bazı uygulamalar barındırıyor. Açıkçası gördüğüm ve beğendiğim özellikleri bir arada toplamaya çalıştım diyebilirim.

Olumlu, olumsuz eleştirilerinizi bekliyorum. Önerileriniz olursa bunları Issue bölümünden iletirseniz sevinirim.

Hoşça kalın :)