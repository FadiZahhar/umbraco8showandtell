// Set initial remaining time
const initialRemainingTime = {
  days: 2,
  hours: 17,
  minutes: 48,
  seconds: 53,
};
 
// Create target date from now
const now = new Date();
const endDate = new Date(
  now.getTime() +
    initialRemainingTime.days * 24 * 60 * 60 * 1000 +
    initialRemainingTime.hours * 60 * 60 * 1000 +
    initialRemainingTime.minutes * 60 * 1000 +
    initialRemainingTime.seconds * 1000
);
 
const countdown = () => {
  const now = new Date();
  const diff = endDate - now;
 
  if (diff <= 0) {
    clearInterval(timer);
    document.querySelectorAll("#countdown .value").forEach(el => el.textContent = "00");
    return;
  }
 
  const seconds = Math.floor((diff / 1000) % 60);
  const minutes = Math.floor((diff / 1000 / 60) % 60);
  const hours = Math.floor((diff / (1000 * 60 * 60)) % 24);
  const days = Math.floor(diff / (1000 * 60 * 60 * 24));
 
  const units = { days, hours, minutes, seconds };
 
  for (const [unit, value] of Object.entries(units)) {
    const el = document.querySelector(`[data-unit="${unit}"] .value`);
    if (el) el.textContent = value.toString().padStart(2, "0");
  }
};
 
const timer = setInterval(countdown, 1000);
countdown(); // initial render