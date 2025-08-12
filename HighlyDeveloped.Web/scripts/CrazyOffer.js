(function () {
  var root = document.getElementById('countdown');
  if (!root) return;

  var endAttr = root.getAttribute('data-end-utc');
  if (!endAttr) return;

  var endDate = new Date(endAttr);
  if (isNaN(endDate)) return;

  function pad(n) { return n < 10 ? '0' + n : '' + n; }

  function render(diffMs) {
    var s = Math.floor(diffMs / 1000);
    var d = Math.floor(s / 86400); s -= d * 86400;
    var h = Math.floor(s / 3600);  s -= h * 3600;
    var m = Math.floor(s / 60);    s -= m * 60;

    var map = {
      days: d,
      hours: pad(h),
      minutes: pad(m),
      seconds: pad(s)
    };

    Object.keys(map).forEach(function(unit){
      var el = root.querySelector('[data-unit="'+unit+'"] .value');
      if (el) el.textContent = map[unit];
    });
  }

  function tick() {
    var now = Date.now();
    var diff = endDate.getTime() - now;

    if (diff <= 0) {
      clearInterval(timer);
      root.querySelectorAll('.value').forEach(function(el){ el.textContent = '00'; });
      return;
    }

    render(diff);
  }

  tick();
  var timer = setInterval(tick, 1000);
})();
