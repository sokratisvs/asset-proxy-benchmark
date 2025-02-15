<script setup>
</script>

<template>
  <main>
    <div :class="theme">
      <h1>Dynamic Asset Loader</h1>
      <!-- Load simple SVG -->
      <img :src="getIconUrl('icon1', 'light')" alt="Icon 1" />

      <!-- Load complex SVG -->
      <img :src="getIconUrl('complexIcon', 'dark')" alt="Complex Icon" />

      <!-- Load brand-based image -->
      <img :src="getBrandImageUrl('brandA', 'light')" alt="Brand A" />
    </div>
  </main>
</template>
