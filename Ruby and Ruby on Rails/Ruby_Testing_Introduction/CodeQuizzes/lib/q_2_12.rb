class Array
  def merge
    inject({}) do |memo, hash|
      hash.each do |k, v|
        (memo[k] ||= []) << v
      end
      memo
    end
  end
end
