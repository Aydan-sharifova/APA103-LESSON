const app = document.getElementById("app");

document.body.style.margin = "0";
document.body.style.background = "#eef3f8";
document.body.style.fontFamily = "Arial, sans-serif";
document.body.style.display = "flex";
document.body.style.justifyContent = "center";
document.body.style.alignItems = "center";
document.body.style.minHeight = "100vh";

const card = document.createElement("div");
Object.assign(card.style, {
  width: "450px",
  background: "#fff",
  borderRadius: "8px",
  overflow: "hidden",
  boxShadow: "0 4px 15px rgba(0,0,0,0.12)"
});

const imageBox = document.createElement("div");
Object.assign(imageBox.style, {
  height: "280px",
  backgroundImage: "url('https://images.unsplash.com/photo-1564013799919-ab600027ffc6')",
  backgroundSize: "cover",
  backgroundPosition: "center",
  position: "relative"
});

const heart = document.createElement("button");
heart.innerHTML = "♡";
Object.assign(heart.style, {
  position: "absolute",
  top: "18px",
  right: "22px",
  fontSize: "40px",
  background: "transparent",
  border: "none",
  color: "white",
  cursor: "pointer"
});

let liked = false;

heart.addEventListener("click", () => {
  liked = !liked;
  heart.innerHTML = liked ? "♥" : "♡";
  heart.style.color = liked ? "red" : "white";
});

const info = document.createElement("div");
Object.assign(info.style, {
  padding: "20px"
});

info.innerHTML = `
  <h3 style="margin:0 0 10px; color:#2d4364; font-size:18px;">
    DETACHED HOUSE • 5Y OLD
  </h3>
  <div style="font-size:36px; margin-bottom:8px;">$750,000</div>
  <div style="font-size:20px; color:#2d4364;">742 Evergreen Terrace</div>
`;

const details = document.createElement("div");
Object.assign(details.style, {
  display: "flex",
  borderTop: "1px solid #ddd",
  borderBottom: "1px solid #ddd"
});

details.innerHTML = `
  <div style="flex:1; padding:22px; font-size:20px; color:#2d4364;">
    🛏️ <b style="color:black;">3</b> Bedrooms
  </div>
  <div style="flex:1; padding:22px; font-size:20px; color:#2d4364;">
    🛁 <b style="color:black;">2</b> Bathrooms
  </div>
`;

const realtor = document.createElement("div");
Object.assign(realtor.style, {
  padding: "20px",
  background: "#f6f9fc"
});

realtor.innerHTML = `
  <h4 style="margin:0 0 15px; color:#6b7890;">REALTOR</h4>
  <div style="display:flex; align-items:center; gap:15px;">
    <img src="https://randomuser.me/api/portraits/women/44.jpg"
         style="width:55px; height:55px; border-radius:50%;">
    <div>
      <div style="font-weight:bold; font-size:20px;">Tiffany Heffner</div>
      <div style="font-size:17px; color:#2d4364;">(555) 555-4321</div>
    </div>
  </div>
`;

card.appendChild(imageBox);
imageBox.appendChild(heart);
card.appendChild(info);
card.appendChild(details);
card.appendChild(realtor);

app.appendChild(card);