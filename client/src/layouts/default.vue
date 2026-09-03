<template>
    <div>
        <NavBar />
        <div class="main">
            <div class="container">
                <slot></slot>
            </div>
        </div>
        <Footer />
    </div>
</template>
<script setup lang="ts">
    import Footer from '~/components/setup/Footer.vue';
    import NavBar from '~/components/setup/NavBar.vue';
</script>
<style>
.main{
    display: flex;
    justify-content: center;
    margin-top:50px;
    width:100%;
    height:100%;
}
.container{
    margin-top:10px;
    margin-bottom:50px;
    border-radius: 5px 5px 0px 0px;
    width:90%;
    background-color: whitesmoke;
    padding:20px;
}
.centered{
  display:flex;
  justify-content: center;
  text-align: center;
  align-items: center;
}
h1 {
	font-family: 'Noto EC Light';
    font-size: 34px;
}
h2,h3,nav {
	font-family: 'Barlow CR';
}
p,li, div{
    font-family: 'Barlow CR';
    font-size: 18px;
}
@media screen and (max-width: 480px) {
  p,li,div {
    font-size: 14px;
  }
}
.primary-text .primary-button {
  border: 2px solid white;
  background: rgba(32, 100, 66, 0.4);
  outline: 1;
  display: inline-block;
  padding: 10px 25px;
  color: black;
  text-align: center;
  cursor: pointer;
}
.primary-text .primary-button:hover {
  background-color: #555;
  color: white;
}
.button-container{
    display:flex;
    justify-content: center;
    gap: 15px;
    flex-direction: row;
}

.margin-bottom-5{
    margin-bottom: 5px;
}
body {
    margin: 0;
    font-family: Arial, Helvetica, sans-serif;
}
</style>