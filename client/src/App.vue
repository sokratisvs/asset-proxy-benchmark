<script setup>
import { ref } from 'vue'
import { RouterLink, RouterView } from 'vue-router'

const theme = ref('light') // reactive variable for theme

const getIconUrl = (iconName, mode) => {
  return `/proxy/${iconName}-${mode}.svg`
}

const getBrandImageUrl = (brand, mode) => {
  return `/images/${brand}/${mode}.jpg`
}
</script>

<template>
  <div class="wrapper" :class="theme">
      <h1>Dynamic Asset Loader</h1>
      <!-- Load simple SVG -->
      <img :src="getIconUrl('icon1', 'light')" alt="Icon 1" />

      <!-- Load complex SVG -->
      <img :src="getIconUrl('complexIcon', 'dark')" alt="Complex Icon" />

      <!-- Load brand-based image -->
      <img :src="getBrandImageUrl('brandA', 'light')" alt="Brand A" />
    </div>

  <!-- <RouterView /> -->
</template>

<style scoped>
div.wrapper {
  width: 100%;
  max-height: 100vh;
  font-size: 12px;
  text-align: center;
  margin-top: 2rem;
}

@media (min-width: 1024px) {
  div .wrapper {
    display: flex;
    flex-direction: column;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}

.light {
  background-color: #fff;
  color: #000;
}

.dark {
  background-color: #333;
  color: #fff;
}
</style>
