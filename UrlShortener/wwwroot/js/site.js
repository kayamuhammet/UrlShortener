// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', function () {
    // Form validation
    (function () {
        'use strict'
        var forms = document.querySelectorAll('.needs-validation')
        Array.prototype.slice.call(forms)
            .forEach(function (form) {
                form.addEventListener('submit', function (event) {
                    if (!form.checkValidity()) {
                        event.preventDefault()
                        event.stopPropagation()
                    }
                    form.classList.add('was-validated')
                }, false)
            })
    })();

    // URL auto-format
    const urlInput = document.getElementById('originalUrl');
    if (urlInput) {
        urlInput.addEventListener('paste', function (e) {
            setTimeout(() => {
                let url = this.value;
                if (url && !url.startsWith('http://') && !url.startsWith('https://')) {
                    this.value = 'https://' + url;
                }
            }, 0);
        });
    }

    // Initialize clipboard.js
    if (typeof ClipboardJS !== 'undefined') {
        var clipboard = new ClipboardJS('.copy-btn');

        clipboard.on('success', function (e) {
            var btn = e.trigger;

            // SweetAlert2 ile bildirim göster
            Swal.fire({
                icon: 'success',
                title: 'Başarılı!',
                text: 'URL kopyalandı',
                timer: 1500,
                showConfirmButton: false,
                position: 'center',
                background: '#fff',
                color: '#28a745',
                width: '24em',
                heightAuto: true,
                showClass: {
                    popup: 'animate__animated animate__fadeIn' 
                },
                hideClass: {
                    popup: 'animate__animated animate__fadeOut'
                }
            });

            btn.classList.remove('btn-outline-secondary');
            btn.classList.add('btn-success');

            setTimeout(function () {
                btn.classList.remove('btn-success');
                btn.classList.add('btn-outline-secondary');
            }, 1500);

            e.clearSelection();
        });

        clipboard.on('error', function (e) {
            console.error('Kopyalama başarısız:', e.action);

            // Hata durumunda SweetAlert2 ile bildirim
            Swal.fire({
                icon: 'error',
                title: 'Hata!',
                text: 'URL kopyalanamadı. Lütfen manuel olarak kopyalayın.',
                position: 'center', 
                timer: 3000,
                showConfirmButton: false,
                background: '#fff', 
                color: '#dc3545', 
                width: '24em' 
            });
        });
    }
});