<!DOCTYPE html>
<html>
<head>
<title>Gra4</title>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css">
<link rel="stylesheet" href="https://fonts.cdnfonts.com/css/electronic-highway-sign">

<style>

body, html {
  height: 150%;
  font-family: "Electronic Highway Sign", sans-serif;
  font-size: 20px;
}

.bgimg {
  background-position: center;
  background-size: cover;
  background-image: url("/w3images/coffeehouse.jpg");
  min-height: 75%;
}

.menu {
  display: none;
}
h4 {
  font-family: "Electronic Highway Sign";
  font-size: 26px;
  font-weight: bold;
}

h5 {
  font-family: "Electronic Highway Sign", sans-serif;
  font-size: 40px;
}

iframe {
  align: center;
}

.w-top{position:fixed;width:100%;z-index:1}
.w-top{top:0}

.w3-content{margin-left:auto;margin-right:auto}
.w3-content{max-width:980px}

.w-container{padding:0.01em 16px}
.w-container:after
.w-container:before

.w3-row:after,.w3-row:before,.w3-row-padding:after,.w3-row-padding:before,

dt {
  font-size: 18px;
}

</style>
</head>


<!-- BODY -->


<body>

<!-- Links (sit on top) -->
<div class="w-top">
  <div class="w3-row w3-padding w3-black">
    <div class="w3-col s3">
      <a href="#" class="w3-button w3-block w3-black">PLAY</a>
    </div>
    <div class="w3-col s3">
      <a href="#trailer" class="w3-button w3-block w3-black">TRAILER</a>
    </div>
    <div class="w3-col s3">
      <a href="#story" class="w3-button w3-block w3-black">STORY</a>
    </div>
    <div class="w3-col s3">
      <a href="#protagonist" class="w3-button w3-block w3-black">PROTAGONIST</a>
    </div>
    <div class="w3-col s3">
      <a href="#enemies" class="w3-button w3-block w3-black">ENEMIES & PROPS</a>
    </div>
    <div class="w3-col s3">
      <a href="#credits" class="w3-button w3-block w3-black">CREDITS</a>
    </div>
  </div>
</div>

<!-- Header with Unity Window -->
<div class="w-container" id="play" style="background-color: #cccccc">
  <div class="w3-content" style="max-width:700px">
    <h5 class="w3-center w3-padding-64"><span class="w3-tag w3-wide">PLAY</span></h5>
    <p>The Cafe.</p>
  </div>
</div>

<!-- Add a background color and large text to the whole page -->
<div class="w3-sand w3-grayscale w3-large">

<!-- TRAILER Container -->
<div class="w-container" id="trailer" style="background-color: #cccccc">
  <div align="center">
    <iframe width="1280" height="720" src="https://www.youtube.com/embed/dQw4w9WgXcQ" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>
  </div>
</div>

<!-- STORY Container -->
<div class="w-container" id="story" style="background-color: #cccccc">
  <div class="w3-content" style="max-width:700px">
    <h5 class="w3-center w3-padding-64"><span class="w3-tag w3-wide">Story</span></h5>
    <p>The Cafe.</p>
    <p>In addition.</p>
  </div>
</div>

<!-- PROTAGONIST Container -->
<div class="w-container" id="protagonist" style="background-color: #cccccc">
  <div class="w3-content" style="max-width:700px">
    <center>
    <h5 class="w3-center w3-padding-64"><span class="w3-tag w3-wide">PROTAGONIST</span></h5>
    <img src="https://cdn.discordapp.com/attachments/1086670234607419543/1086670703794868334/SpriteMC.gif">
    <p>Berry Jug</p>
    </center>
  </div>
</div>

<!-- ENEMIES Container -->
<div class="w-container" id="enemies" style="background-color: #cccccc">
  <div class="w3-content" style="max-width:800px">

    <h5 class="w3-center w3-padding-48"><span class="w3-tag w3-wide">ENEMIES & PROPS</span></h5>

    <div class="w3-row w3-center w3-card w3-padding">
      <a href="javascript:void(0)" onclick="openMenu(event, 'scientist');" id="myLink">
        <div class="w3-col s6 tablink">Scientist</div>
      </a>
      <a href="javascript:void(0)" onclick="openMenu(event, 'probe');">
        <div class="w3-col s6 tablink">Probe Droid</div>
      </a>
      <a href="javascript:void(0)" onclick="openMenu(event, 'stomper');">
        <div class="w3-col s6 tablink">Stomper</div>
      </a>
      <a href="javascript:void(0)" onclick="openMenu(event, 'jugjug');">
        <div class="w3-col s6 tablink">JugJug</div>
      </a>
      <a href="javascript:void(0)" onclick="openMenu(event, 'orb');">
        <div class="w3-col s6 tablink">Orb</div>
      </a>
      <a href="javascript:void(0)" onclick="openMenu(event, 'portal');">
        <div class="w3-col s6 tablink">Portal</div>
      </a>
    </div>

    <div id="scientist" class="w-container menu w3-padding-48 w3-card">
      <h5>Scientist</h5>
      <img src="https://twistedsifter.com/wp-content/uploads/2016/03/scientist-street-fighter-game-pixel-art-animation-by-diego-sanches-4.gif">
      <p>Assortment of fresh baked fruit breads and muffins 5.50</p>

    </div>

    <div id="probe" class="w-container menu w3-padding-48 w3-card">
      <h5>Probe Droid</h5>
      <p class="w3-text-grey">Regular coffee 2.50</p><br>
    </div>

    <div id="stomper" class="w-container menu w3-padding-48 w3-card">
      <h5>Stomper</h5>
    </div>

    <div id="jugjug" class="w-container menu w3-padding-48 w3-card">
      <h5>JugJug</h5>
    </div>

    <div id="orb" class="w-container menu w3-padding-48 w3-card">
      <h5>Orb</h5>
    </div>

    <div id="portal" class="w-container menu w3-padding-48 w3-card">
      <h5>Portal</h5>
    </div>
  </div>
</div>

<!-- CREDITS Container -->
<div class="w-container" id="credits" style="padding-bottom:32px; background-color: #cccccc">
  <div class="w3-content" style="max-width:700px">
    <h5 class="w3-center w3-padding-48"><span class="w3-tag w3-wide">CREDITS</span></h5>
    <center>

    <h4>CREW</h4>
    <dl>
      <dt>Ole Beisswenger</dt>
      <dt>Timm Reutemann</dt>
      <dt>Stephen Tafferner</dt>
      <dt>An Trieu</dt>
      <dt>Marco Cramer</dt>
      <dt>Tarik Vischer</dt>
    </dl>

    <br>

    <h4>Sprites</h4>
    <dl>
      <dt>Ansimuz</dt>
      <dt>Timm Reutemann</dt>
      <dt>Stephen Tafferner</dt>
      <dt>Marco Cramer</dt>
    </dl>

    <br>

    <h4>Music</h4>
    <dl>
      <dt>Lui Knapp</dt>
    </dl>

    <br>

    <h4>Special Thanks</h4>
    <dl>
      <dt>Ansimuz</dt>
      <dt>Lui Knapp</dt>
    </dl>
  </center>
  </div>
</div>

<!-- Footer -->

<script>
// Tabbed Menu
function openMenu(evt, menuName) {
  var i, x, tablinks;
  x = document.getElementsByClassName("menu");
  for (i = 0; i < x.length; i++) {
    x[i].style.display = "none";
  }
  tablinks = document.getElementsByClassName("tablink");
  for (i = 0; i < x.length; i++) {
    tablinks[i].className = tablinks[i].className.replace(" w3-dark-grey", "");
  }
  document.getElementById(menuName).style.display = "block";
  evt.currentTarget.firstElementChild.className += " w3-dark-grey";
}
document.getElementById("myLink").click();
</script>

</body>
</html>
