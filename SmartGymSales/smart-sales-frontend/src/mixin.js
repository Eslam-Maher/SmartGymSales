export default {
  methods: {
    invalidFilter(val) {
      return val.match(/[^a-zA-Z0-9,.'/?-\s]|(\s\s+)/g) ? true : false;
    }
  },
  computed: {},
  filters: {}
};
