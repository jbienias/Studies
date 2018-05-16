require 'q_2_10'

describe "#symbolize" do
  it "creates an array of symbols" do
    arr = %w|b c d c a|
    expected = [:b, :c, :d, :c, :a]
    expect(arr.symbolize).to eq expected
  end
end
