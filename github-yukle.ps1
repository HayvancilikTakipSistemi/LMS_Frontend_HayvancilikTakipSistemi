# GitHub Yükleme Yardımcı Betiği
# Bu dosyayı sağ tıklayıp "Run with PowerShell" diyerek çalıştırın.

$RepoUrl = Read-Host "Lütfen GitHub depo URL'nizi yapıştırın (Örn: https://github.com/kullanici/repo.git)"

if (-not (Test-Path .git)) {
    Write-Host "Git deposu başlatılıyor..." -ForegroundColor Cyan
    git init
}

Write-Host "GitHub uzak sunucusu ayarlanıyor..." -ForegroundColor Cyan
# Eğer origin varsa temizleyip yenisini ekleyelim
git remote remove origin 2>$null
git remote add origin $RepoUrl

Write-Host "Değişiklikler paketleniyor..." -ForegroundColor Cyan
git add .
git commit -m "Sprint 2: Dashboard ve Veri Senkronizasyonu tamamlandı"

Write-Host "GitHub'a gönderiliyor... (Giriş yapmanız istenebilir)" -ForegroundColor Yellow
git push -u origin master

Write-Host "İşlem Tamamlandı! Lütfen GitHub sayfanızı kontrol edin." -ForegroundColor Green
pause
