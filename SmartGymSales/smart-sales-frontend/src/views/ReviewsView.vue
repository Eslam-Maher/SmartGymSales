<template>
  <review-grid :reviews="reviews"></review-grid>
</template>

<script>
import reviewGrid from "../components/Reviews/ReviewGrid";
import reviewService from "../services/review";
export default {
  components: { reviewGrid },
  data() {
    return {
      reviews: []
    };
  },
  methods: {
    getReviews: function() {
      this.loadingCount++;
      reviewService
        .GetReviews()
        .then(res => {
          this.reviews = res.data;
        })
        .catch(error => {// eslint-disable-line no-unused-vars
          this.$bvToast.toast(
            "error in getting reviews ",
            this.failToastConfig
          );
        })
        .finally(() => {
          this.loadingCount--;
        });
    }
  },
  created: function() {
        if (!this.isManger) {
      this.$router.push({ name: "home" });
      return;
    }
    this.getReviews();
  }
};
</script>

<style>
</style>